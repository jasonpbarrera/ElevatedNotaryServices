const COMPANY_NAME = "Elevated Notary Services";
const DEFAULT_RECIPIENT_EMAIL = "info@jbdigitalservices.net";
const DEFAULT_SENDER_EMAIL = "info@jbdigitalservices.net";

function escapeHtml(value) {
  return String(value ?? "")
    .replace(/&/g, "&amp;")
    .replace(/</g, "&lt;")
    .replace(/>/g, "&gt;")
    .replace(/"/g, "&quot;")
    .replace(/'/g, "&#39;");
}

function getCaseInsensitive(obj, key) {
  const lower = key.toLowerCase();
  for (const [k, v] of Object.entries(obj)) {
    if (k.toLowerCase() === lower) {
      return typeof v === "string" ? v : v == null ? "" : String(v);
    }
  }
  return "";
}

async function handleLeads(request, env) {
  if (request.method !== "POST") {
    return new Response("Method Not Allowed", {
      status: 405,
      headers: { Allow: "POST" },
    });
  }

  let raw;
  try {
    raw = await request.json();
  } catch (err) {
    console.warn("Invalid JSON payload on /api/leads", err);
    return new Response(null, { status: 400 });
  }

  if (!raw || typeof raw !== "object" || Array.isArray(raw)) {
    return new Response(null, { status: 400 });
  }

  const input = {
    companyName: getCaseInsensitive(raw, "companyName") || COMPANY_NAME,
    clientCategory: getCaseInsensitive(raw, "clientCategory") || "General Inquiry",
    organization: getCaseInsensitive(raw, "organization"),
    name: getCaseInsensitive(raw, "name"),
    phone: getCaseInsensitive(raw, "phone"),
    email: getCaseInsensitive(raw, "email"),
    serviceType:
      getCaseInsensitive(raw, "serviceType") ||
      getCaseInsensitive(raw, "service"),
    location: getCaseInsensitive(raw, "location"),
    preferredDate: getCaseInsensitive(raw, "preferredDate"),
    message: getCaseInsensitive(raw, "message"),
    honeypot: getCaseInsensitive(raw, "honeypot"),
  };

  // Server-side honeypot check: silently return 200 OK if filled by a bot
  if (input.honeypot.trim().length > 0) {
    return new Response(null, { status: 200 });
  }

  if (!input.name.trim() || !input.email.trim() || !input.phone.trim()) {
    return new Response("Name, Email, and Phone are required.", {
      status: 400,
      headers: { "content-type": "text/plain; charset=utf-8" },
    });
  }

  const brevoApiKey = (env.BREVO_API_KEY || "").trim();
  const isDemoMode = String(env.DEMO_MODE || "").toLowerCase() === "true";

  // Allow demo submissions to succeed gracefully before BREVO_API_KEY is attached
  if (!brevoApiKey) {
    if (isDemoMode) {
      return new Response(
        JSON.stringify({
          status: "saved",
          demo: true,
          id: crypto.randomUUID(),
          message: "Demo request captured successfully.",
        }),
        {
          status: 201,
          headers: { "content-type": "application/json; charset=utf-8" },
        }
      );
    }
    console.error("Missing BREVO_API_KEY secret.");
    return new Response(null, { status: 500 });
  }

  const recipientEmail =
    (env.LEAD_NOTIFICATION_EMAIL || "").trim() || DEFAULT_RECIPIENT_EMAIL;
  const senderEmail =
    (env.BREVO_SENDER_EMAIL || "").trim() || DEFAULT_SENDER_EMAIL;

  const serviceType = input.serviceType.trim() || "Signing / Notary Inquiry";

  const htmlBody = `
    <div style='font-family:sans-serif;max-width:620px;color:#0b192c;'>
      <h2 style='color:#0b192c;border-bottom:2px solid #c5a059;padding-bottom:8px;'>
        New Website Request — ${escapeHtml(COMPANY_NAME)}
      </h2>
      <table style='border-collapse:collapse;width:100%;font-size:15px;'>
        <tr><td style='padding:8px 12px;font-weight:bold;width:180px;'>Category:</td><td style='padding:8px 12px;'>${escapeHtml(input.clientCategory.trim())}</td></tr>
        <tr style='background:#f8f7f3;'><td style='padding:8px 12px;font-weight:bold;'>Name:</td><td style='padding:8px 12px;'>${escapeHtml(input.name.trim())}</td></tr>
        ${input.organization.trim() ? `<tr><td style='padding:8px 12px;font-weight:bold;'>Company / Title Co:</td><td style='padding:8px 12px;'>${escapeHtml(input.organization.trim())}</td></tr>` : ""}
        <tr style='background:#f8f7f3;'><td style='padding:8px 12px;font-weight:bold;'>Phone:</td><td style='padding:8px 12px;'>${escapeHtml(input.phone.trim())}</td></tr>
        <tr><td style='padding:8px 12px;font-weight:bold;'>Email:</td><td style='padding:8px 12px;'>${escapeHtml(input.email.trim())}</td></tr>
        <tr style='background:#f8f7f3;'><td style='padding:8px 12px;font-weight:bold;'>Service Needed:</td><td style='padding:8px 12px;'>${escapeHtml(serviceType)}</td></tr>
        ${input.location.trim() ? `<tr><td style='padding:8px 12px;font-weight:bold;'>Signing Location:</td><td style='padding:8px 12px;'>${escapeHtml(input.location.trim())}</td></tr>` : ""}
        ${input.preferredDate.trim() ? `<tr style='background:#f8f7f3;'><td style='padding:8px 12px;font-weight:bold;'>Target Date/Time:</td><td style='padding:8px 12px;'>${escapeHtml(input.preferredDate.trim())}</td></tr>` : ""}
        <tr><td style='padding:8px 12px;font-weight:bold;vertical-align:top;'>Details:</td><td style='padding:8px 12px;white-space:pre-wrap;'>${escapeHtml(input.message.trim() || "No additional notes provided.")}</td></tr>
      </table>
      <p style='margin-top:16px;font-size:13px;color:#64748b;'>
        Reply directly to this email to respond to ${escapeHtml(input.name.trim())} at ${escapeHtml(input.email.trim())}.
      </p>
    </div>`;

  const payload = {
    sender: { name: COMPANY_NAME, email: senderEmail },
    to: [{ email: recipientEmail, name: COMPANY_NAME }],
    replyTo: { email: input.email.trim(), name: input.name.trim() },
    subject: `[ENS Request] ${serviceType} — ${input.name.trim()}`,
    htmlContent: htmlBody,
  };

  try {
    const emailResp = await fetch("https://api.brevo.com/v3/smtp/email", {
      method: "POST",
      headers: {
        "api-key": brevoApiKey,
        "content-type": "application/json",
        accept: "application/json",
      },
      body: JSON.stringify(payload),
    });

    if (!emailResp.ok) {
      const errBody = await emailResp.text();
      console.error(`Brevo returned ${emailResp.status}: ${errBody}`);
      return new Response(null, { status: 502 });
    }
  } catch (err) {
    console.error("Failed to send inquiry email via Brevo.", err);
    return new Response(null, { status: 502 });
  }

  return new Response(
    JSON.stringify({ status: "saved", id: crypto.randomUUID() }),
    {
      status: 201,
      headers: { "content-type": "application/json; charset=utf-8" },
    }
  );
}

export default {
  async fetch(request, env) {
    const url = new URL(request.url);
    const pathname = url.pathname.replace(/\/+$/, "") || "/";

    if (pathname === "/api/leads") {
      return handleLeads(request, env);
    }

    if (pathname.startsWith("/api/")) {
      return new Response("Not Found", { status: 404 });
    }

    return env.ASSETS.fetch(request);
  },
};
