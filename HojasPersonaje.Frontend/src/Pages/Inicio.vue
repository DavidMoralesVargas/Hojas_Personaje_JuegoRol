<<template>
  <div class="home-container">
    <AppHeader /> <main class="hero-section">
      <div class="hero-content">
        <h2 class="hero-title">La Sangre lo es Todo</h2>
        <p class="hero-description">
          Bienvenido a las sombras. Administra tus crónicas, gestiona las fichas de tus personajes y sumérgete en la intriga política de los Vástagos. La Mascarada debe ser protegida.
        </p>
        <div class="hero-actions">
          <button class="btn-primary">Crear Personaje</button>
          <button class="btn-secondary">Unirse a una Crónica</button>
        </div>
      </div>
      
      <div class="ambient-glow"></div>
    </main>
  </div>
</template>

<script setup>
import AppHeader from '../Components/Header.vue' // Ajusta la ruta según tu proyecto
</script>

<style scoped>
.home-container {
  min-height: 100vh;
  /* Fondo principal: Un degradado que va de un gris oscuro a un gris más carbón, no es negro puro */
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
  text-align: center;
  max-width: 800px;
  z-index: 2; /* Para que quede por encima del brillo ambiental */
}

.hero-title {
  font-family: 'Cinzel', serif;
  font-size: 4rem;
  color: #fff;
  margin-bottom: 1rem;
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.8);
}

.hero-description {
  font-family: 'Open Sans', sans-serif;
  font-size: 1.2rem;
  line-height: 1.6;
  color: #b0b0b0;
  margin-bottom: 3rem;
}

.hero-actions {
  display: flex;
  gap: 2rem;
  justify-content: center;
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
  background-color: #720e0e; /* Rojo oscuro */
  color: #ffffff;
  border: none;
  box-shadow: 0 4px 10px rgba(114, 14, 14, 0.4);
}

.btn-primary:hover {
  background-color: #9c1515;
  box-shadow: 0 6px 15px rgba(156, 21, 21, 0.6);
  transform: translateY(-2px);
}

.btn-secondary {
  background-color: rgba(43, 43, 48, 0.6);
  color: #d4d4d4;
  border: 1px solid #4a4a4f;
}

.btn-secondary:hover {
  background-color: rgba(60, 60, 65, 0.8);
  border-color: #720e0e;
  color: #fff;
  transform: translateY(-2px);
}

/* Efecto de luz ambiental difusa (estilo gótico/vampírico) */
.ambient-glow {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 60vw;
  height: 60vw;
  background: radial-gradient(circle, rgba(138, 3, 3, 0.05) 0%, rgba(0, 0, 0, 0) 70%);
  pointer-events: none; /* Para que no interfiera con los clics */
  z-index: 1;
}
</style>>