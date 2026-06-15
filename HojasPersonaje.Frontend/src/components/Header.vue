<template>
  <header class="vampire-header">
    
    <div class="header-left">
      <div class="logo-container">
        <h1 class="logo-text">V:TM</h1>
      </div>

      <nav class="main-nav">
        <a href="#" class="nav-link">Ver hojas de personaje</a>
        
        <div v-if="isAuthenticated && isAdmin" class="dropdown" ref="dropdownContainer">
          <button class="dropdown-toggle" @click="toggleDropdown">
            Administración {{ isDropdownOpen ? '▴' : '▾' }}
          </button>
          
          <div class="dropdown-menu" :class="{ 'is-open': isDropdownOpen }">
            <a href="/disciplinas" class="dropdown-item" @click="isDropdownOpen = false">Disciplinas</a>
            <a href="/vampiros" class="dropdown-item" @click="isDropdownOpen = false">Vampiros</a>
          </div>
        </div>
      </nav>
    </div>

    <div class="header-right">
      <button v-if="!isAuthenticated" class="btn-abrazar" @click="goToLogin">
        Abrazar
      </button>
      
      <button v-else class="btn-logout" @click="logout">Salir</button>
    </div>

  </header>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const isAuthenticated = ref(false)
const isAdmin = ref(false)

// Estado para controlar si el menú está abierto o cerrado
const isDropdownOpen = ref(false)
const dropdownContainer = ref(null)

// Función para alternar el menú al hacer clic
const toggleDropdown = () => {
  isDropdownOpen.value = !isDropdownOpen.value
}

// Función para cerrar el menú si se hace clic en cualquier otra parte de la pantalla
const closeDropdownOutside = (event) => {
  if (dropdownContainer.value && !dropdownContainer.value.contains(event.target)) {
    isDropdownOpen.value = false
  }
}

onMounted(() => {
  if (localStorage.getItem('Token')) {
    isAuthenticated.value = true
    
    const userRole = localStorage.getItem('Rol')
    if (userRole === 'Administrador') {
      isAdmin.value = true
    }
  }

  // Escuchamos los clics globales para poder cerrar el menú al hacer clic fuera
  window.addEventListener('click', closeDropdownOutside)
})

onUnmounted(() => {
  // Limpiamos el evento cuando el componente se destruye para evitar fugas de memoria
  window.removeEventListener('click', closeDropdownOutside)
})

const goToLogin = () => {
  router.push('/login') 
}

const logout = () => {
  localStorage.removeItem('Token')
  localStorage.removeItem('Rol')
  isAuthenticated.value = false
  isAdmin.value = false
  isDropdownOpen.value = false
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
  align-items: center;
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

/* === ESTILOS DEL DROPDOWN POR CLICK === */
.dropdown {
  position: relative;
  display: inline-block;
}

.dropdown-toggle {
  background: none;
  border: none;
  color: #a0a0a0;
  font-family: 'Open Sans', sans-serif;
  text-transform: uppercase;
  font-size: 0.9rem;
  letter-spacing: 1px;
  cursor: pointer;
  padding: 0;
  transition: color 0.3s ease;
}

.dropdown-toggle:hover {
  color: #e21c1c;
}

.dropdown-menu {
  display: none; /* Por defecto oculto */
  position: absolute;
  top: 100%;
  left: 0;
  background-color: rgba(20, 20, 22, 0.95);
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.6);
  border: 1px solid #5a0202;
  z-index: 1;
  margin-top: 0.5rem;
  border-radius: 3px;
}

/* Esta clase es la que se añade dinámicamente con Vue cuando isDropdownOpen es true */
.dropdown-menu.is-open {
  display: block;
}

.dropdown-item {
  color: #d4d4d4;
  padding: 12px 16px;
  text-decoration: none;
  display: block;
  font-family: 'Open Sans', sans-serif;
  font-size: 0.85rem;
  transition: background-color 0.2s ease, color 0.2s ease;
}

.dropdown-item:hover {
  background-color: #5a0202;
  color: #fff;
}

/* === BOTONES EXISTENTES === */
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
    flex-direction: column;
    gap: 1rem;
  }

  .dropdown-menu {
    position: static;
    box-shadow: none;
    background-color: rgba(35, 35, 38, 0.95);
  }

  .header-right {
    width: 100%;
    display: flex;
    justify-content: center;
  }
}
</style>