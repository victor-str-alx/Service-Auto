document.addEventListener("DOMContentLoaded", () => {
  const user = JSON.parse(localStorage.getItem("serviceAutoUser") || "null");
  const profileName = document.getElementById("profile-name");
  const profileEmail = document.getElementById("profile-email");
  const profilePhone = document.getElementById("profile-phone");
  const vehiclesList = document.getElementById("vehicles-list");
  const vehiclesEmpty = document.getElementById("vehicles-empty");
  const vehicleForm = document.getElementById("vehicle-form");
  const vehicleIdInput = document.getElementById("vehicle-id");
  const vehicleBrandInput = document.getElementById("vehicle-brand");
  const vehicleModelInput = document.getElementById("vehicle-model");
  const vehicleRegistrationInput = document.getElementById("vehicle-registration");
  const vehicleSaveBtn = document.getElementById("vehicle-save-btn");
  const vehicleCancelBtn = document.getElementById("vehicle-cancel-btn");
  const vehicleFeedback = document.getElementById("vehicle-feedback");
  const appointmentsList = document.getElementById("appointments-list");
  const appointmentsEmpty = document.getElementById("appointments-empty");

  if (!user) {
    window.location.href = "login.html";
    return;
  }

  if (profileName) profileName.textContent = user.name || "Utilizator";
  if (profileEmail) profileEmail.textContent = user.email || "-";
  if (profilePhone) profilePhone.textContent = user.phone || "Nespecificat";

  const getVehicles = async () => {
    if (!user.email) return [];
    const response = await fetch(`http://localhost:5099/api/profile/vehicles?email=${encodeURIComponent(user.email)}`);
    if (!response.ok) return [];
    return response.json();
  };

  const setVehicleFeedback = (message, type = "success") => {
    if (!vehicleFeedback) return;
    vehicleFeedback.className = `alert alert-${type} py-2`;
    vehicleFeedback.textContent = message;
  };

  const resetVehicleForm = () => {
    if (vehicleForm) vehicleForm.reset();
    if (vehicleIdInput) vehicleIdInput.value = "";
    if (vehicleSaveBtn) vehicleSaveBtn.textContent = "Adauga masina";
    vehicleCancelBtn?.classList.add("d-none");
  };

  const getAppointments = async () => {
    if (!user.email) return [];
    const response = await fetch(`http://localhost:5099/api/profile/appointments?email=${encodeURIComponent(user.email)}`);
    if (!response.ok) return [];
    return response.json();
  };

  const renderVehicles = async () => {
    if (!vehiclesList || !vehiclesEmpty) return;

    const vehicles = await getVehicles();
    vehiclesList.innerHTML = "";

    if (!vehicles.length) {
      vehiclesList.classList.add("d-none");
      vehiclesEmpty.classList.remove("d-none");
      return;
    }

    vehiclesEmpty.classList.add("d-none");
    vehiclesList.classList.remove("d-none");

    vehicles.forEach((vehicle) => {
      const li = document.createElement("li");
      li.className = "mb-2 d-flex justify-content-between align-items-center border rounded p-2";

      const text = document.createElement("span");
      text.textContent = `${vehicle.brand} ${vehicle.model} | Nr. ${vehicle.registrationNumber}`;

      const actions = document.createElement("div");
      actions.className = "d-flex gap-2";

      const editBtn = document.createElement("button");
      editBtn.type = "button";
      editBtn.className = "btn btn-outline-primary btn-sm";
      editBtn.textContent = "Editeaza";
      editBtn.addEventListener("click", () => {
        if (vehicleIdInput) vehicleIdInput.value = String(vehicle.id);
        if (vehicleBrandInput) vehicleBrandInput.value = vehicle.brand;
        if (vehicleModelInput) vehicleModelInput.value = vehicle.model;
        if (vehicleRegistrationInput) vehicleRegistrationInput.value = vehicle.registrationNumber;
        if (vehicleSaveBtn) vehicleSaveBtn.textContent = "Salveaza modificari";
        vehicleCancelBtn?.classList.remove("d-none");
      });

      const deleteBtn = document.createElement("button");
      deleteBtn.type = "button";
      deleteBtn.className = "btn btn-outline-danger btn-sm";
      deleteBtn.textContent = "Sterge";
      deleteBtn.addEventListener("click", async () => {
        if (!confirm("Esti sigur ca vrei sa stergi acest vehicul?")) return;
        try {
          const response = await fetch("http://localhost:5099/api/profile/vehicles", {
            method: "DELETE",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
              email: user.email,
              vehicleId: vehicle.id
            })
          });
          if (!response.ok) throw new Error("Nu am putut sterge vehiculul.");
          setVehicleFeedback("Vehicul sters cu succes.");
          await renderVehicles();
        } catch (error) {
          setVehicleFeedback(error?.message || "Eroare la stergere.", "danger");
        }
      });

      actions.appendChild(editBtn);
      actions.appendChild(deleteBtn);
      li.appendChild(text);
      li.appendChild(actions);
      vehiclesList.appendChild(li);
    });
  };

  const renderAppointments = async () => {
    if (!appointmentsList || !appointmentsEmpty) return;
    const appointments = await getAppointments();
    appointmentsList.innerHTML = "";

    if (!appointments.length) {
      appointmentsList.classList.add("d-none");
      appointmentsEmpty.classList.remove("d-none");
      return;
    }

    appointmentsEmpty.classList.add("d-none");
    appointmentsList.classList.remove("d-none");

    appointments.forEach((appointment) => {
      const li = document.createElement("li");
      li.className = "mb-2 d-flex justify-content-between align-items-center border rounded p-2";
      const date = new Date(appointment.appointmentDate);
      const vehicleText = appointment.vehicle
        ? `${appointment.vehicle.brand} ${appointment.vehicle.model} (${appointment.vehicle.registrationNumber})`
        : "Vehicul necunoscut";
      const text = document.createElement("span");
      text.textContent = `${date.toLocaleDateString("ro-RO")} | ${appointment.timeSlot || "-"} | ${appointment.serviceName || "-"} | ${vehicleText}`;

      const cancelBtn = document.createElement("button");
      cancelBtn.type = "button";
      cancelBtn.className = "btn btn-outline-danger btn-sm";
      cancelBtn.textContent = "Anuleaza";
      cancelBtn.addEventListener("click", async () => {
        if (!confirm("Esti sigur ca vrei sa anulezi aceasta programare?")) return;
        try {
          const response = await fetch(`http://localhost:5099/api/profile/appointments?email=${encodeURIComponent(user.email)}&appointmentId=${appointment.id}`, {
            method: "DELETE"
          });
          if (!response.ok) throw new Error("Nu am putut anula programarea.");
          await renderAppointments();
        } catch (error) {
          alert(error?.message || "Eroare la anulare programare.");
        }
      });

      li.appendChild(text);
      li.appendChild(cancelBtn);
      appointmentsList.appendChild(li);
    });
  };

  vehicleCancelBtn?.addEventListener("click", () => {
    resetVehicleForm();
  });

  vehicleForm?.addEventListener("submit", async (event) => {
    event.preventDefault();
    const vehicleId = Number(vehicleIdInput?.value || 0);
    const brand = vehicleBrandInput?.value?.trim();
    const model = vehicleModelInput?.value?.trim();
    const registration = vehicleRegistrationInput?.value?.trim();
    if (!brand || !model || !registration) {
      setVehicleFeedback("Completeaza toate campurile.", "danger");
      return;
    }

    try {
      const method = vehicleId ? "PUT" : "POST";
      const payload = vehicleId
        ? {
            email: user.email,
            vehicleId,
            brand,
            model,
            registrationNumber: registration
          }
        : {
            email: user.email,
            brand,
            model,
            registrationNumber: registration
          };

      const response = await fetch("http://localhost:5099/api/profile/vehicles", {
        method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
      });
      if (!response.ok) {
        throw new Error("Nu am putut salva modificarile pentru vehicul.");
      }
      setVehicleFeedback(vehicleId ? "Vehicul actualizat." : "Vehicul adaugat.");
      resetVehicleForm();
      await renderVehicles();
      await renderAppointments();
    } catch (error) {
      setVehicleFeedback(error?.message || "Eroare la salvare vehicul.", "danger");
    }
  });

  renderVehicles();
  renderAppointments();
});
