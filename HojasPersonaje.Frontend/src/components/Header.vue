<template>
  <header class="vampire-header">
    <div class="logo-container">
      <h1 class="logo-text">V:TM <span class="subtitle">Crónicas</span></h1>
    </div>
    
    <nav class="main-nav">
      <ul>
        <li><a href="#" class="nav-link">El Dominio</a></li>
        <li><a href="#" class="nav-link">Códice</a></li>
        <li><a href="#" class="nav-link">Clanes</a></li>
      </ul>
    </nav>

    <div class="auth-options">
      <button class="btn-login">Entrar a la Noche</button>
      <button class="btn-register">Abrazar (Registro)</button>
    </div>
  </header>
</template>

<script setup>
// Aquí puedes agregar la lógica de autenticación o enrutamiento (vue-router)
</script>

<style scoped>
.vampire-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1rem 2rem;
  background-color: rgba(28, 28, 30, 0.95); /* Gris oscuro casi negro con ligera transparencia */
  border-bottom: 2px solid #5a0202; /* Borde rojo oscuro/sangre */
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.5);
  position: sticky;
  top: 0;
  z-index: 100;
}

.logo-text {
  font-family: 'Cinzel', serif; /* Te recomiendo usar una fuente serif elegante como Cinzel de Google Fonts */
  color: #d4d4d4;
  font-size: 1.5rem;
  margin: 0;
  letter-spacing: 2px;
}

.subtitle {
  color: #8a0303; /* Rojo sangre */
  font-size: 1rem;
}

.main-nav ul {
  display: flex;
  list-style: none;
  gap: 2rem;
  margin: 0;
  padding: 0;
}

.nav-link {
  color: #a0a0a0;
  text-decoration: none;
  font-family: 'Open Sans', sans-serif;
  text-transform: uppercase;
  font-size: 0.9rem;
  letter-spacing: 1px;
  transition: color 0.3s ease;
}

.nav-link:hover {
  color: #e21c1c;
  text-shadow: 0 0 8px rgba(226, 28, 28, 0.4);
}

.auth-options {
  display: flex;
  gap: 1rem;
}

.btn-login, .btn-register {
  font-family: 'Open Sans', sans-serif;
  padding: 0.5rem 1rem;
  cursor: pointer;
  border-radius: 3px;
  font-weight: bold;
  transition: all 0.3s ease;
}

.btn-login {
  background: transparent;
  color: #d4d4d4;
  border: 1px solid #5a0202;
}

.btn-login:hover {
  background: rgba(90, 2, 2, 0.2);
  color: #fff;
}

.btn-register {
  background: #8a0303;
  color: #fff;
  border: 1px solid #8a0303;
}

.btn-register:hover {
  background: #5a0202;
  border-color: #5a0202;
}
</style>