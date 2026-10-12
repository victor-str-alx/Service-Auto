document.addEventListener("DOMContentLoaded", async () => {
  const currentUser = JSON.parse(localStorage.getItem("serviceAutoUser") || "null");
  const form = document.getElementById("appointment-form");
  const feedback = document.getElementById("appointment-feedback");
  const fullNameInput = document.getElementById("fullName");
  const phoneInput = document.getElementById("phone");
  const emailInput = document.getElementById("email");
  const vehicleSelect = document.getElementById("vehicleId");
  const vehicleHelp = document.getElementById("vehicle-help");

  if (!form || !feedback) {
    return;
  }

  if (!currentUser) {
    feedback.classList.remove("d-none");
    feedback.classList.add("alert", "alert-warning");
    feedback.innerHTML = 'Trebuie sa fii autentificat pentru programare. <a href="login.html">Autentificare</a> sau <a href="register.html">Inregistrare</a>.';
    Array.from(form.elements).forEach((el) => {
      el.disabled = true;
    });
    return;
  }

  if (fullNameInput && currentUser.name) fullNameInput.value = currentUser.name;
  if (phoneInput && currentUser.phone) phoneInput.value = currentUser.phone;
  if (emailInput && currentUser.email) emailInput.value = currentUser.email;

  const loadVehicles = async () => {
    if (!vehicleSelect || !currentUser.email) return [];
    const response = await fetch(`http://localhost:5099/api/profile/vehicles?email=${encodeURIComponent(currentUser.email)}`);
    if (!response.ok) {
      return [];
    }
    return response.json();
  };

  const disableFormWithVehicleMessage = () => {
    if (vehicleHelp) {
      vehicleHelp.classList.remove("d-none");
      vehicleHelp.innerHTML = 'Nu ai masini in profil. Adauga o masina din <a href="profile.html">Profil</a> si revino.';
    }
    Array.from(form.elements).forEach((el) => {
      el.disabled = true;
    });
  };

  try {
    const vehicles = await loadVehicles();
    if (!vehicleSelect) return;
    vehicleSelect.innerHTML = "";
    if (!vehicles.length) {
      vehicleSelect.innerHTML = '<option value="">-- Nu ai masini --</option>';
      disableFormWithVehicleMessage();
      return;
    }

    vehicleSelect.innerHTML = '<option value="">-- Alege masina --</option>';
    vehicles.forEach((v) => {
      const opt = document.createElement("option");
      opt.value = v.id;
      opt.textContent = `${v.brand} ${v.model} (${v.registrationNumber})`;
      vehicleSelect.appendChild(opt);
    });
  } catch {
    feedback.classList.remove("d-none");
    feedback.classList.add("alert-danger");
    feedback.textContent = "Nu am putut incarca masinile din profil.";
    return;
  }

  form.addEventListener("submit", async (event) => {
    event.preventDefault();

    const payload = {
      fullName: document.getElementById("fullName")?.value?.trim(),
      phone: document.getElementById("phone")?.value?.trim(),
      email: document.getElementById("email")?.value?.trim(),
      preferredDate: document.getElementById("preferredDate")?.value,
      timeSlot: document.getElementById("timeSlot")?.value,
      serviceName: document.getElementById("serviceName")?.value,
      vehicleId: Number(vehicleSelect?.value),
      notes: document.getElementById("notes")?.value?.trim() || null
    };

    if (!payload.vehicleId) {
      feedback.classList.remove("d-none", "alert-success");
      feedback.classList.add("alert-danger");
      feedback.textContent = "Selecteaza mai intai o masina din lista.";
      return;
    }

    try {
      const response = await fetch("http://localhost:5099/api/appointments", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(payload)
      });

      if (!response.ok) {
        throw new Error("Backend error");
      }

      const result = await response.json();
      if (!response.ok) {
        throw new Error(result.message || "Backend error");
      }
      feedback.classList.remove("d-none", "alert-danger");
      feedback.classList.add("alert-success");
      feedback.textContent = `${result.message} ID programare: ${result.appointmentId}.`;

      form.reset();
      if (fullNameInput && currentUser.name) fullNameInput.value = currentUser.name;
      if (phoneInput && currentUser.phone) phoneInput.value = currentUser.phone;
      if (emailInput && currentUser.email) emailInput.value = currentUser.email;
    } catch (error) {
      feedback.classList.remove("d-none", "alert-success");
      feedback.classList.add("alert-danger");
      feedback.textContent = error?.message || "Nu s-a putut salva programarea. Verifica daca backend-ul ruleaza pe http://localhost:5099.";
    }
  });
});
