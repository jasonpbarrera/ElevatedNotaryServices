# Elevated Notary Services — Custom Web Platform Demo

Custom **.NET 10 Blazor WebAssembly + PWA** platform designed and built by **JB Digital Services** (`https://jbdigitalservices.net`) for **Elevated Notary Services** (Cleveland, TN & 50-State Nationwide Signing Coordination).

## Live Demo Target
- **Custom Subdomain:** `https://elevatednotary.jbdigitalservices.net`
- **Cloudflare Worker:** `elevated-notary-services` (Cloudflare Workers with Static Assets — Free Tier)

## Architecture Highlights
- **Dual-Audience Experience:** Separate high-intent pathways for B2B Title & Lender partners (Notary Everyday portal integration, 1-click signing dispatch, $1M E&O credentials) and local Southeast Tennessee / RON individual clients.
- **SEO & Crawlers:** Per-route static HTML SEO shells (`scripts/generate-route-shells.py`), JSON-LD `LocalBusiness` & `WebSite` schema, `1200×630` branded Open Graph card, `sitemap.xml`, `robots.txt`, and IndexNow submissions.
- **PWA Ready:** Installable on desktop and mobile home screens for 1-tap order dispatch.
- **Serverless Dispatch API:** Cloudflare Worker (`worker/index.js`) handles `POST /api/leads` with honeypot protection and Brevo transactional email (graceful demo mode enabled until `BREVO_API_KEY` is attached).

## Local Build & Publish
```bash
dotnet publish Client/ElevatedNotaryServices.Client.csproj -c Release -o publish_output
python3 scripts/generate-route-shells.py publish_output/wwwroot
```
