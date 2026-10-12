import { chromium } from 'playwright';

const outDir = new URL('./screenshots/', import.meta.url).pathname;

const pages = [
  { url: 'http://localhost:8000/index.html', name: '01-frontend-acasa' },
  { url: 'http://localhost:8000/services.html', name: '02-frontend-servicii' },
  { url: 'http://localhost:8000/appointments.html', name: '03-frontend-programari' },
  { url: 'http://localhost:8000/login.html', name: '04-frontend-login' },
  { url: 'http://localhost:8000/register.html', name: '05-frontend-inregistrare' },
  { url: 'http://localhost:5099/', name: '06-mvc-home' },
  { url: 'http://localhost:5099/ServiceTypes', name: '07-mvc-servicii' },
  { url: 'http://localhost:5099/Customers', name: '08-mvc-clienti' },
  { url: 'http://localhost:5099/Vehicles', name: '09-mvc-vehicule' },
  { url: 'http://localhost:5099/Employees', name: '10-mvc-angajati' },
  { url: 'http://localhost:5099/Appointments', name: '11-mvc-programari' },
];

const browser = await chromium.launch();
const page = await browser.newPage({ viewport: { width: 1280, height: 800 } });

for (const item of pages) {
  await page.goto(item.url, { waitUntil: 'networkidle', timeout: 15000 });
  await page.screenshot({ path: `${outDir}${item.name}.png`, fullPage: true });
  console.log('saved', item.name);
}

await browser.close();
