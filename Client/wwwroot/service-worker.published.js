// Caution! Be sure you understand the caveats before publishing an application with
// offline support. See https://aka.ms/blazor-offline-considerations

self.importScripts('./service-worker-assets.js');
self.addEventListener('install', event => event.waitUntil(onInstall(event)));
self.addEventListener('activate', event => event.waitUntil(onActivate(event)));
self.addEventListener('fetch', event => event.respondWith(onFetch(event)));

const cacheNamePrefix = 'offline-cache-';
const cacheName = `${cacheNamePrefix}${self.assetsManifest.version}`;
const offlineAssetsInclude = [ /\.dll$/, /\.pdb$/, /\.wasm/, /\.html/, /\.js$/, /\.json$/, /\.css$/, /\.woff$/, /\.png$/, /\.jpe?g$/, /\.webp$/, /\.gif$/, /\.ico$/, /\.blat$/, /\.dat$/, /\.webmanifest$/ ];
const offlineAssetsExclude = [ /^service-worker\.js$/ ];

async function onInstall(event) {
    self.skipWaiting();
    const assetsRequests = self.assetsManifest.assets
        .filter(asset => offlineAssetsInclude.some(pattern => pattern.test(asset.url)))
        .filter(asset => !offlineAssetsExclude.some(pattern => pattern.test(asset.url)))
        .map(asset => new Request(asset.url, { integrity: asset.hash, cache: 'no-cache' }));
    await caches.open(cacheName).then(cache => cache.addAll(assetsRequests));
}

async function onActivate(event) {
    const cacheKeys = await caches.keys();
    await Promise.all(cacheKeys
        .filter(key => key.startsWith(cacheNamePrefix) && key !== cacheName)
        .map(key => caches.delete(key)));
    await self.clients.claim();
}

async function onFetch(event) {
    const req = event.request;
    if (req.method !== 'GET' || req.url.includes('/api/')) {
        return fetch(req);
    }

    const url = new URL(req.url);
    if (url.origin !== self.location.origin) {
        return fetch(req);
    }

    const isNavigation = req.mode === 'navigate';
    const isDynamicAsset = isNavigation
        || url.pathname.endsWith('.html')
        || url.pathname.endsWith('.css');

    // Network-first for HTML navigation & CSS using URL string + redirect:'follow'.
    // Strip the .redirected flag when Cloudflare redirects /route -> /route/ so browsers never reject navigate FetchEvents.
    if (isDynamicAsset) {
        try {
            const networkResponse = await fetch(req.url, { cache: 'no-cache', redirect: 'follow' });
            if (networkResponse && networkResponse.ok) {
                if (isNavigation && networkResponse.redirected) {
                    return new Response(networkResponse.body, {
                        status: networkResponse.status,
                        statusText: networkResponse.statusText,
                        headers: networkResponse.headers
                    });
                }
                return networkResponse;
            }
        } catch {
            // Offline: fall back to cached copy below
        }
    }

    const cache = await caches.open(cacheName);
    const cacheKey = isNavigation ? 'index.html' : req;
    const cachedResponse = await cache.match(cacheKey);
    if (cachedResponse) {
        return cachedResponse;
    }

    try {
        return await fetch(req.url, { redirect: 'follow' });
    } catch {
        const fallback = await cache.match('index.html');
        return fallback || Response.error();
    }
}
