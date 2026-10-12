/**
 * Service Auto - Script principal
 * Gestionează afișarea utilizatorului conectat (simulat) și activează link-urile din navbar
 */

document.addEventListener('DOMContentLoaded', function() {
  // Utilizator simulat (pentru demo - în aplicație reală vine din backend/sesiune)
  const currentUser = JSON.parse(localStorage.getItem('serviceAutoUser')) || null;

  const userInfoEl = document.getElementById('user-info-nav');
  const loginRegisterEl = document.getElementById('login-register-nav');

  if (userInfoEl && loginRegisterEl) {
    if (currentUser) {
      loginRegisterEl.classList.add('d-none');
      userInfoEl.classList.remove('d-none');
      const nameEl = userInfoEl.querySelector('.user-name');
      const emailEl = userInfoEl.querySelector('.user-email');
      if (nameEl) nameEl.textContent = currentUser.name || 'Utilizator';
      if (emailEl) emailEl.textContent = currentUser.email || '';
    } else {
      userInfoEl.classList.add('d-none');
      loginRegisterEl.classList.remove('d-none');
    }
  }

  // Marchează link-ul activ în navbar în funcție de pagina curentă
  const currentPage = window.location.pathname.split('/').pop() || 'index.html';
  document.querySelectorAll('.navbar-service .nav-link[href]').forEach(function(link) {
    const href = link.getAttribute('href');
    if (href === currentPage || (currentPage === '' && href === 'index.html')) {
      link.classList.add('active');
    } else {
      link.classList.remove('active');
    }
  });
});
