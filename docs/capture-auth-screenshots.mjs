import { chromium } from 'playwright';

const outDir = new URL('./screenshots/', import.meta.url).pathname;
const user = {
  name: 'Ion Popescu',
  email: 'ion.popescu@email.ro',
  phone: '0721000000'
};

const browser = await chromium.launch();
const page = await browser.newPage({ viewport: { width: 1280, height: 800 } });

await page.goto('http://localhost:8000/index.html', { waitUntil: 'domcontentloaded' });
await page.evaluate((u) => localStorage.setItem('serviceAutoUser', JSON.stringify(u)), user);
await page.goto('http://localhost:8000/profile.html', { waitUntil: 'networkidle' });
await page.screenshot({ path: `${outDir}12-frontend-profil.png`, fullPage: true });
console.log('saved 12-frontend-profil');

await page.goto('http://localhost:8000/appointments.html', { waitUntil: 'networkidle' });
await page.screenshot({ path: `${outDir}13-frontend-programari-autentificat.png`, fullPage: true });
console.log('saved 13-frontend-programari-autentificat');

await browser.close();
