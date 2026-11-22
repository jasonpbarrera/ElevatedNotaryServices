#!/usr/bin/env python3
"""
Generate per-route static HTML SEO shells in the published Blazor WASM wwwroot directory.
Ensures search crawlers and social link-preview bots receive route-specific metadata
and canonical URLs without waiting for WebAssembly hydration.
"""
import os
import re
import sys
from pathlib import Path

BASE_URL = "https://elevatednotary.jbdigitalservices.net"

ROUTES = {
    "title-lender-solutions": {
        "title": "Title & Lender Signing Solutions | Elevated Notary Services",
        "description": "50-state loan signing coordination for title companies, lenders, escrow officers, and attorneys. $1,000,000 E&O insured, background-checked signing agents, and Notary Everyday portal integration.",
    },
    "services": {
        "title": "Mobile Notary, RON & Individual Services | Elevated Notary Services",
        "description": "Mobile notary services in Cleveland, TN & Southeast Tennessee, Remote Online Notarization (RON) nationwide, walk-in storefront notarizations, jail notary, and wedding officiant services.",
    },
    "request-signing": {
        "title": "Request a Signing or Notary Appointment | Elevated Notary Services",
        "description": "Submit a nationwide loan signing request or book a local mobile notary, Remote Online Notarization (RON), or storefront appointment with Elevated Notary Services.",
    },
    "notary-partners": {
        "title": "Notary Partners — Join Our Network | Elevated Notary Services",
        "description": "Join the Elevated Notary Services nationwide signing network. Independent contractor opportunities for vetted loan signing agents, RON notaries, and mobile notaries.",
    },
    "about": {
        "title": "About Elevated Notary Services | Brittany & Jimmy Smith — Cleveland, TN",
        "description": "Meet Brittany and Jimmy Smith, co-owners of Elevated Notary Services in Cleveland, Tennessee. Built on reliability, proactive communication, and $1,000,000 E&O insured signing standards.",
    },
    "faqs": {
        "title": "Frequently Asked Questions | Elevated Notary Services",
        "description": "Answers to common questions about Elevated Notary Services, nationwide loan signing coordination, Cleveland TN walk-in and mobile notary appointments, and Remote Online Notarization (RON).",
    },
    "contact": {
        "title": "Contact & Cleveland, TN Storefront | Elevated Notary Services",
        "description": "Visit Elevated Notary Services at 130 Bernham Dr NW, Cleveland, TN 37312 (walk-ins welcome), call (423) 375-8770, or send us a message.",
    },
    "privacy": {
        "title": "Privacy Policy | Elevated Notary Services",
        "description": "Privacy Policy for Elevated Notary Services covering website inquiries, signing coordination, payment processing, and client data protection.",
    },
    "terms": {
        "title": "Terms of Service | Elevated Notary Services",
        "description": "Terms of Service for Elevated Notary Services including notarial disclaimers, appointment policies, billing, and Tennessee jurisdiction.",
    },
}


def esc_attr(val: str) -> str:
    return (
        val.replace("&", "&amp;")
        .replace('"', "&quot;")
        .replace("<", "&lt;")
        .replace(">", "&gt;")
    )


def main():
    wwwroot = Path(sys.argv[1] if len(sys.argv) > 1 else "publish_output/wwwroot")
    index_path = wwwroot / "index.html"
    if not index_path.exists():
        print(f"ERROR: {index_path} not found.", file=sys.stderr)
        sys.exit(1)

    base_html = index_path.read_text(encoding="utf-8")

    for route, meta in ROUTES.items():
        canonical = f"{BASE_URL}/{route}"
        title = esc_attr(meta["title"])
        desc = esc_attr(meta["description"])

        html = base_html
        html = re.sub(r"<title>.*?</title>", f"<title>{title}</title>", html, count=1, flags=re.S)
        html = re.sub(
            r'<meta name="description" content="[^"]*"\s*/?>',
            f'<meta name="description" content="{desc}" />',
            html,
            count=1,
        )
        html = re.sub(
            r'<link rel="canonical" href="[^"]*"\s*/?>',
            f'<link rel="canonical" href="{canonical}" />',
            html,
            count=1,
        )
        html = re.sub(
            r'<meta property="og:title" content="[^"]*"\s*/?>',
            f'<meta property="og:title" content="{title}" />',
            html,
            count=1,
        )
        html = re.sub(
            r'<meta property="og:description" content="[^"]*"\s*/?>',
            f'<meta property="og:description" content="{desc}" />',
            html,
            count=1,
        )
        html = re.sub(
            r'<meta property="og:url" content="[^"]*"\s*/?>',
            f'<meta property="og:url" content="{canonical}" />',
            html,
            count=1,
        )
        html = re.sub(
            r'<meta name="twitter:title" content="[^"]*"\s*/?>',
            f'<meta name="twitter:title" content="{title}" />',
            html,
            count=1,
        )
        html = re.sub(
            r'<meta name="twitter:description" content="[^"]*"\s*/?>',
            f'<meta name="twitter:description" content="{desc}" />',
            html,
            count=1,
        )

        out_dir = wwwroot / route
        out_dir.mkdir(parents=True, exist_ok=True)
        (out_dir / "index.html").write_text(html, encoding="utf-8")

    print(f"Generated {len(ROUTES)} static SEO route shells in {wwwroot}")


if __name__ == "__main__":
    main()
