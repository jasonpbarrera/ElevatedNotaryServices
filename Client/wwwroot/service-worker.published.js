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
    if (event.request.method !== 'GET' || event.request.url.includes('/api/')) {
        return fetch(event.request);
    }

    const isNavigation = event.request.mode === 'navigate';
    const isDynamicAsset = isNavigation
        || event.request.url.endsWith('.html')
        || event.request.url.endsWith('.css');

    // Network-first for HTML navigation and CSS so new deployments never serve stale fingerprinted asset hashes
    if (isDynamicAsset) {
        try {
            const networkResponse = await fetch(event.request, { cache: 'no-cache' });
            if (networkResponse && networkResponse.ok) {
                return networkResponse;
            }
        } catch {
            // Offline: fall through to cached copy
        }
    }

    const cache = await caches.open(cacheName);
    const request = isNavigation ? 'index.html' : event.request;
    const cachedResponse = await cache.match(request);
    return cachedResponse || fetch(event.request);
}
