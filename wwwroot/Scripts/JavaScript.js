"use strict"

window.onload = () => {
    const observer = new IntersectionObserver(entries =>
        entries.forEach(i => i.isIntersecting && i.target.classList.add("show")));
    const observerTargets = document.querySelectorAll(".observer-target");
    observerTargets.length > 0 && observerTargets.forEach(i => observer.observe(i));
};