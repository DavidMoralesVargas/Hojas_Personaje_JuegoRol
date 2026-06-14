<template>
  <div class="home-container">
    <AppHeader />

    <main class="hero-section">
      
      <!-- VISTA: Sin Token (Mensaje de ambientación) -->
      <div v-if="!isAuthenticated" class="hero-content text-center">
        <h2 class="hero-title">Ecos en la Oscuridad</h2>
        <p class="hero-description atmospheric-text">
          Las calles huelen a sangre derramada y a furia salvaje. Ya seas un Vástago atado a la Mascarada o un Garou aullando bajo la luz de Selene, la noche reclama lo suyo. Las sombras te aguardan... ¿Sobrevivirás a la caza?
        </p>
      </div>

      <!-- VISTA: Con Token (Botones de Crónica) -->
      <div v-else class="hero-content text-center">
        <h2 class="hero-title">Tu Dominio</h2>
        <div class="action-buttons">
          <button class="btn-primary">Crear Crónica</button>
          <button class="btn-secondary">Unirse a Crónica</button>
        </div>
      </div>
      
      <!-- Luz de fondo para ambiente gótico -->
      <div class="ambient-glow"></div>
    </main>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import AppHeader from '../components/Header.vue'

const isAuthenticated = ref(false)

onMounted(() => {
  if (localStorage.getItem('Token')) {
    isAuthenticated.value = true
  }
})
</script>

<style scoped>
.home-container {
  min-height: 100vh;
  background: radial-gradient(circle at center, #2b2b30 0%, #121215 80%, #0a0a0c 100%);
  color: #e0e0e0;
  display: flex;
  flex-direction: column;
}

.hero-section {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
  position: relative;
  overflow: hidden;
  padding: 2rem;
}

.hero-content {
  max-width: 800px;
  z-index: 2;
  text-align: center;
}

.hero-title {
  font-family: 'Cinzel', serif;
  font-size: 3.5rem;
  color: #fff;
  margin-bottom: 1.5rem;
  text-shadow: 2px 2px 5px rgba(0, 0, 0, 0.9);
}

.hero-description {
  font-family: 'Open Sans', sans-serif;
  font-size: 1.2rem;
  line-height: 1.8;
  color: #c4c4c4;
}

.atmospheric-text {
  font-style: italic;
  border-top: 1px solid #5a0202;
  border-bottom: 1px solid #5a0202;
  padding: 1.5rem 0;
  background: rgba(0, 0, 0, 0.2);
}

.action-buttons {
  display: flex;
  gap: 2rem;
  justify-content: center;
  margin-top: 2rem;
}

.btn-primary, .btn-secondary {
  padding: 1rem 2rem;
  font-size: 1.1rem;
  font-family: 'Cinzel', serif;
  cursor: pointer;
  border-radius: 4px;
  transition: all 0.4s ease;
  letter-spacing: 1px;
}

.btn-primary {
  background-color: #720e0e;
  color: #ffffff;
  border: none;
  box-shadow: 0 4px 10px rgba(114, 14, 14, 0.4);
}

.btn-primary:hover {
  background-color: #9c1515;
  transform: translateY(-2px);
}

.btn-secondary {
  background-color: rgba(43, 43, 48, 0.6);
  color: #d4d4d4;
  border: 1px solid #720e0e;
}

.btn-secondary:hover {
  background-color: rgba(60, 60, 65, 0.9);
  color: #fff;
  transform: translateY(-2px);
  box-shadow: 0 4px 10px rgba(114, 14, 14, 0.2);
}

.ambient-glow {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 70vw;
  height: 70vw;
  background: radial-gradient(circle, rgba(138, 3, 3, 0.08) 0%, rgba(0, 0, 0, 0) 65%);
  pointer-events: none;
  z-index: 1;
}

/* === RESPONSIVE DESIGN === */
@media (max-width: 768px) {
  .hero-title {
    font-size: 2.2rem;
  }

  .hero-description {
    font-size: 1rem;
    padding: 1rem;
  }

  .action-buttons {
    flex-direction: column;
    gap: 1rem;
    width: 100%;
    padding: 0 1rem;
  }

  .btn-primary, .btn-secondary {
    width: 100%;
  }
}
</style>