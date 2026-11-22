namespace ElevatedNotaryServices.Client.Data;

public sealed record ArticleSection(string Heading, string[] Paragraphs, string[]? Bullets = null);

public sealed record BlogArticle(
    string Slug,
    string Title,
    string Subtitle,
    string Category,
    string ReadTime,
    string PublishedDate,
    bool IsNew,
    string Excerpt,
    ArticleSection[] Sections
);

public static class BlogRepository
{
    public static readonly BlogArticle[] Articles =
    {
        new(
            Slug: "notarial-certificates-and-risks",
            Title: "When \"Just in Case\" Becomes a Risk: Blank Notarial Certificates",
            Subtitle: "Why professional signing agents protect every closing by tying each notarial certificate to an actual notarial act.",
            Category: "Notary Compliance",
            ReadTime: "4 min read",
            PublishedDate: "August 31, 2026",
            IsNew: false,
            Excerpt: "In real estate closings, signing agents sometimes receive requests to include a blank notarial certificate \"just in case.\" Here is why strict notarial compliance protects lenders, title companies, and signers alike.",
            Sections: new[]
            {
                new ArticleSection(
                    "A Certificate Must Correspond to an Actual Notarial Act",
                    new[]
                    {
                        "In the mortgage and real estate industry, signing agents receive detailed instructions designed to keep closings moving smoothly. Occasionally, a request asks the notary to include a blank notarial certificate \"just in case\" a stamp or wording is missed.",
                        "A notarial certificate is never just a spare form — it is an official written record of a specific notarial act that actually occurred in the notary's presence. An acknowledgment documents an acknowledgment; a jurat documents an oath or affirmation."
                    }
                ),
                new ArticleSection(
                    "Why Pre-Signed or Blank Certificates Create Liability",
                    new[]
                    {
                        "Providing a loose, blank, or pre-stamped certificate ahead of time separates the notary's seal from the actual signing event, creating compliance exposure for the notary, the title company, and the lender.",
                        "If an administrative correction is ever needed after a signing, it must be addressed properly under state notarial law rather than by attaching loose certificates."
                    },
                    new[]
                    {
                        "Every certificate must match the exact signer, date, venue, and notarial act performed.",
                        "Pre-delivery scanback quality control catches missing dates, venues, or seals before funding.",
                        "Strict compliance protects the enforceability of recorded deeds and security instruments."
                    }
                )
            }
        ),
        new(
            Slug: "trusted-notary-signing-agent-cleveland-tn",
            Title: "Trusted Notary Signing Agent in Cleveland, TN for Real Estate Closings",
            Subtitle: "How experienced signing coordination prevents funding delays for title companies, lenders, and real estate teams.",
            Category: "Title & Lenders",
            ReadTime: "5 min read",
            PublishedDate: "August 2026",
            IsNew: false,
            Excerpt: "Every real estate closing carries tight funding deadlines. Learn what separates an experienced, $1M E&O insured signing service from an unvetted notary.",
            Sections: new[]
            {
                new ArticleSection(
                    "Why the Right Signing Professional Matters",
                    new[]
                    {
                        "In today's fast-moving real estate market, every closing matters. Deadlines are tight, communication is critical, and even a minor execution error — a missed initial, an incomplete date, or a delayed scanback — can stall funding.",
                        "Not all notaries have the same level of experience with complex loan packages. Elevated Notary Services pairs local Cleveland, TN expertise with nationwide 50-state signing coordination."
                    },
                    new[]
                    {
                        "Purchase & seller closing packages",
                        "Refinance, cash-out, and HELOC signings",
                        "Reverse mortgages, loan modifications, and commercial closings",
                        "Same-day, evening, weekend, and scanback-verified returns"
                    }
                ),
                new ArticleSection(
                    "Built-In Quality Control & $1,000,000 E&O Coverage",
                    new[]
                    {
                        "With over 6 years of closing experience and $1,000,000 in Errors & Omissions coverage, ENS treats every file with structured oversight — confirming borrower appointments, verifying signing instructions in advance, and reviewing returned packages for apparent completeness before delivery."
                    }
                )
            }
        ),
        new(
            Slug: "tennessee-notary-ethics-integrity",
            Title: "Tennessee Notary Ethics & Integrity",
            Subtitle: "The core standards that protect signers, prevent fraud, and uphold public trust in every notarization.",
            Category: "Notary Compliance",
            ReadTime: "4 min read",
            PublishedDate: "July 2026",
            IsNew: false,
            Excerpt: "A Notary Public serves as an impartial public official. Explore the ethical principles — impartiality, accuracy, identity verification, and confidentiality — that guide every ENS assignment.",
            Sections: new[]
            {
                new ArticleSection(
                    "Core Procedures That Protect Every Signing",
                    new[]
                    {
                        "Every notarization follows statutory procedures designed to deter fraud, prevent coercion, and ensure documents are executed properly."
                    },
                    new[]
                    {
                        "Verifying the signer's identity using acceptable, unexpired government-issued photo ID",
                        "Confirming the signer appears willingly and understands they are signing the document",
                        "Administering an oath or affirmation whenever required by a jurat",
                        "Completing the applicable notarial certificate truthfully and accurately"
                    }
                ),
                new ArticleSection(
                    "Core Ethical Principles for Tennessee Notaries",
                    new[]
                    {
                        "1. Impartiality: A notary must remain strictly neutral, never favoring one party over another and declining any notarization where a prohibited personal or financial interest exists.",
                        "2. Honesty & Accuracy: Every date, venue, and certificate entry must reflect what actually occurred at the time of signing.",
                        "3. Confidentiality & Professionalism: Real estate packages, wills, and powers of attorney contain sensitive personal and financial data that must be safeguarded at every step."
                    }
                )
            }
        ),
        new(
            Slug: "notary-role-faq",
            Title: "What Is a Notary Public's Job, Really? (Notary Role FAQ & ID Checklist)",
            Subtitle: "Clearing up common myths about notarization and exactly what to bring to your appointment.",
            Category: "Signer Guides",
            ReadTime: "4 min read",
            PublishedDate: "July 2026",
            IsNew: false,
            Excerpt: "Does notarization make a document legally valid? Can a notary pick your certificate? Here is a clear guide to the notary's impartial role plus your ID checklist.",
            Sections: new[]
            {
                new ArticleSection(
                    "Clearing Up Common Notary Misconceptions",
                    new[]
                    {
                        "Many people assume a notary's stamp makes a document \"legal\" or verifies that the statements inside are true. In reality, a Notary Public serves as an impartial witness who verifies identity, confirms willingness, and completes the requested notarial act."
                    },
                    new[]
                    {
                        "Does notarization automatically make a document legally enforceable? No — it verifies signer identity and execution; legal validity is determined by applicable law or an attorney.",
                        "Does a notary read or verify document accuracy? No — advising on legal meaning or document sufficiency constitutes the unauthorized practice of law.",
                        "Can a notary tell me which notarial act I need? No — if you are unsure whether you need an acknowledgment or a jurat, ask the document drafter or receiving agency before your appointment."
                    }
                ),
                new ArticleSection(
                    "Your ID Checklist: What to Bring",
                    new[]
                    {
                        "To meet Tennessee's requirement for satisfactory evidence of identity, bring an unexpired government-issued photo ID (or one issued within the last five years where permitted) that includes a photograph and signature:"
                    },
                    new[]
                    {
                        "State-issued Driver's License or State Identification Card",
                        "Valid U.S. Passport or Passport Card",
                        "U.S. Military Identification Card",
                        "Bring your complete document unsigned — always wait to sign in front of the notary."
                    }
                )
            }
        ),
        new(
            Slug: "ron-for-title-companies-tennessee",
            Title: "Remote Online Notarization (RON) for Title Companies in Tennessee",
            Subtitle: "Streamlining eligible real estate closings with secure, compliant remote notarization.",
            Category: "Title & Lenders",
            ReadTime: "4 min read",
            PublishedDate: "June 2026",
            IsNew: false,
            Excerpt: "When signers are traveling, out of state, or on tight timelines, Remote Online Notarization (RON) gives title and lending teams a secure, flexible alternative for eligible closings.",
            Sections: new[]
            {
                new ArticleSection(
                    "How RON Supports a More Flexible Closing Process",
                    new[]
                    {
                        "Traditional real estate closings require coordinating borrowers, sellers, title officers, and signing agents across physical locations. When permitted by applicable state law and accepted by the lender and title underwriter, Remote Online Notarization (RON) allows eligible signers to complete notarization over an encrypted audio-video session.",
                        "Working with a dedicated signing service like Elevated Notary Services gives title teams a single point of contact to coordinate both in-person and RON assignments seamlessly."
                    }
                ),
                new ArticleSection(
                    "Key Benefits for Title & Lending Teams",
                    new[]
                    {
                        "RON removes geographic bottlenecks while maintaining strict identity verification and tamper-evident digital seals."
                    },
                    new[]
                    {
                        "Scheduling Flexibility: Easily accommodate out-of-state, traveling, or military signers.",
                        "Reduced Travel & Courier Delays: Eliminates shipping turnaround on eligible digital packages.",
                        "Convenient Signer Experience: Guided online session from a smartphone, tablet, or computer.",
                        "Centralized Coordination: Managed end-to-end by ENS's coordination desk."
                    }
                )
            }
        ),
        new(
            Slug: "mobile-notary-vs-remote-online-notary",
            Title: "Mobile Notary vs. Remote Online Notarization (RON): Which Is Right for You?",
            Subtitle: "A side-by-side guide to choosing between an in-person mobile visit, a walk-in storefront signing, or an online RON session.",
            Category: "Signer Guides",
            ReadTime: "5 min read",
            PublishedDate: "June 2026",
            IsNew: false,
            Excerpt: "Not sure whether to book a mobile notary to your door or complete your notarization online? Compare wet-ink requirements, speed, convenience, and technology needs.",
            Sections: new[]
            {
                new ArticleSection(
                    "When to Choose Mobile or Storefront Notarization",
                    new[]
                    {
                        "In-person notarization is ideal when physical wet-ink signatures are required, multiple signers or witnesses are involved, or a signer prefers face-to-face assistance."
                    },
                    new[]
                    {
                        "Physical wet-ink documents required by a county recorder, court, or lender",
                        "Multiple signers, witnesses, or large real estate / estate planning binders",
                        "Hospital, assisted living, or detention center visits where technology access is limited",
                        "Walk-in convenience at our Cleveland, TN office (130 Bernham Dr NW)"
                    }
                ),
                new ArticleSection(
                    "When to Choose Remote Online Notarization (RON)",
                    new[]
                    {
                        "Remote Online Notarization connects you with a Tennessee-commissioned online notary over a secure video platform — perfect when speed and convenience matter most."
                    },
                    new[]
                    {
                        "You are traveling, out of state, or need a fast turnaround without commuting",
                        "You have a smartphone or computer with camera, microphone, and reliable internet",
                        "Your receiving agency or title company confirms acceptance of electronic notarization"
                    }
                )
            }
        ),
        new(
            Slug: "title-escrow-closing-readiness-checklist",
            Title: "Title & Escrow Closing Readiness Checklist: Preventing Funding Delays",
            Subtitle: "Five operational habits title teams and signing services use to achieve zero-defect loan signings.",
            Category: "Title & Lenders",
            ReadTime: "4 min read",
            PublishedDate: "November 2026",
            IsNew: true,
            Excerpt: "A practical pre-closing checklist for escrow officers and lenders to eliminate last-minute print delays, missing initials, and FedEx cutoff misses.",
            Sections: new[]
            {
                new ArticleSection(
                    "Why Pre-Closing Alignment Eliminates 90% of Signing Issues",
                    new[]
                    {
                        "Most funding delays do not happen at the closing table — they happen in the hours leading up to it when document delivery, witness rules, or scanback requirements aren't confirmed early.",
                        "At Elevated Notary Services, our coordination workflow verifies five critical checkpoints on every order before our signing agent ever leaves for the table."
                    },
                    new[]
                    {
                        "Confirm exact signer legal names match government IDs and vesting instructions.",
                        "Verify state-specific witness requirements (especially for deeds and POAs) in advance.",
                        "Flag scanback-before-drop requirements and courier cutoff times up front.",
                        "Ensure closing packages arrive with adequate print-and-review lead time.",
                        "Keep a direct escalation contact available during evening and weekend signings."
                    }
                )
            }
        ),
        new(
            Slug: "estate-planning-poa-signing-guide-tennessee",
            Title: "Preparing for Estate Planning & Power of Attorney Signings in Southeast Tennessee",
            Subtitle: "How families in Cleveland and Bradley County can prepare for smooth, stress-free trust, will, and POA notarizations.",
            Category: "Signer Guides",
            ReadTime: "4 min read",
            PublishedDate: "November 2026",
            IsNew: true,
            Excerpt: "Signing a Power of Attorney, Living Will, or Family Trust is an important milestone. Here is how to prepare IDs, witnesses, and documents so your appointment goes smoothly.",
            Sections: new[]
            {
                new ArticleSection(
                    "Preparing Your Documents & Witnesses Ahead of Time",
                    new[]
                    {
                        "Estate planning signings — whether at our private Cleveland closing room, at your kitchen table, or at a local hospital — go fastest when everyone knows what to expect."
                    },
                    new[]
                    {
                        "Keep all signature lines blank until the notary and required witnesses are present.",
                        "Confirm whether your attorney's package requires one or two disinterested witnesses (family members named in the document typically cannot serve as witnesses).",
                        "Ensure the signer has a valid, unexpired photo ID and is alert, comfortable, and willing to sign.",
                        "Let us know in advance if the appointment is at a medical or assisted-living facility so we can coordinate arrival times seamlessly."
                    }
                )
            }
        )
    };

    public static BlogArticle? FindBySlug(string slug) =>
        Articles.FirstOrDefault(a => string.Equals(a.Slug, slug, StringComparison.OrdinalIgnoreCase));
}
