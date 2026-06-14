<template>
  <header class="vampire-header">
    
    <div class="header-left">
      <div class="logo-container">
        <h1 class="logo-text">V:TM</h1>
      </div>

      <nav class="main-nav">
        <a href="#" class="nav-link">Ver hojas de personaje</a>
        <!-- AQUÍ PUEDES COLOCAR LAS DEMÁS OPCIONES DEL MENÚ -->
      </nav>
    </div>

    <div class="header-right">
      <!-- Agregamos el evento @click="goToLogin" -->
      <button v-if="!isAuthenticated" class="btn-abrazar" @click="goToLogin">
        Abrazar
      </button>
      
      <button v-else class="btn-logout" @click="logout">Salir</button>
    </div>

  </header>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router' // 1. Importar el router

// 2. Inicializar el router
const router = useRouter()
const isAuthenticated = ref(false)

onMounted(() => {
  if (localStorage.getItem('Token')) {
    isAuthenticated.value = true
  }
})

// 3. Crear la función para ir al login
const goToLogin = () => {
  // Asegúrate de que '/login' coincida con la ruta definida en tu router (ej. router/index.js)
  router.push('/login') 
}

const logout = () => {
  localStorage.removeItem('Token')
  isAuthenticated.value = false
  window.location.reload()
}
</script>

<style scoped>
.vampire-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1rem 2rem;
  background-color: rgba(28, 28, 30, 0.95);
  border-bottom: 2px solid #5a0202;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.5);
  position: sticky;
  top: 0;
  z-index: 100;
}

.header-left {
  display: flex;
  align-items: center;
  gap: 2rem;
}

.logo-text {
  font-family: 'Cinzel', serif;
  color: #d4d4d4;
  font-size: 1.5rem;
  margin: 0;
  letter-spacing: 2px;
}

.main-nav {
  display: flex;
  gap: 1.5rem;
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

.btn-abrazar {
  font-family: 'Cinzel', serif;
  background: #8a0303;
  color: #fff;
  border: 1px solid #8a0303;
  padding: 0.5rem 1.5rem;
  font-size: 1rem;
  letter-spacing: 1px;
  cursor: pointer;
  border-radius: 3px;
  transition: all 0.3s ease;
}

.btn-abrazar:hover {
  background: #5a0202;
  border-color: #5a0202;
  box-shadow: 0 0 10px rgba(138, 3, 3, 0.5);
}

.btn-logout {
  background: transparent;
  color: #d4d4d4;
  border: 1px solid #5a0202;
  padding: 0.4rem 1rem;
  cursor: pointer;
  border-radius: 3px;
}

/* === RESPONSIVE DESIGN === */
@media (max-width: 768px) {
  .vampire-header {
    flex-direction: column;
    gap: 1rem;
    padding: 1rem;
  }

  .header-left {
    flex-direction: column;
    gap: 1rem;
    width: 100%;
    text-align: center;
  }

  .main-nav {
    flex-wrap: wrap;
    justify-content: center;
  }

  .header-right {
    width: 100%;
    display: flex;
    justify-content: center;
  }
}
</style>