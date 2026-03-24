/* =============================================
   Kevin Callaert – Portfolio Website
   js/main.js
   ============================================= */

/**
 * Scroll animaties via IntersectionObserver
 * Elementen met de klasse .fade-up worden zichtbaar
 * zodra ze in het viewport scrollen.
 */
const observer = new IntersectionObserver(
  (entries) => {
    entries.forEach((entry) => {
      if (entry.isIntersecting) {
        entry.target.classList.add('visible');
      }
    });
  },
  { threshold: 0.1 }
);

document.querySelectorAll('.fade-up').forEach((el) => observer.observe(el));


/**
 * Actieve navigatielink highlighten op basis van scroll positie.
 * Markeert de juiste link als de sectie zichtbaar is.
 */
const sections = document.querySelectorAll('section[id], div[id]');
const navLinks = document.querySelectorAll('.nav-links a');

const sectionObserver = new IntersectionObserver(
  (entries) => {
    entries.forEach((entry) => {
      if (entry.isIntersecting) {
        navLinks.forEach((link) => {
          link.style.color = '';
          if (link.getAttribute('href') === '#' + entry.target.id) {
            link.style.color = 'var(--text)';
          }
        });
      }
    });
  },
  { threshold: 0.4 }
);

sections.forEach((section) => sectionObserver.observe(section));


/**
 * Smooth navigatie: voeg actieve klasse toe bij klikken op nav-links.
 */
navLinks.forEach((link) => {
  link.addEventListener('click', () => {
    navLinks.forEach((l) => (l.style.color = ''));
    link.style.color = 'var(--text)';
  });
});
