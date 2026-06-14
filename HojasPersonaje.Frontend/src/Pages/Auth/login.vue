<template>
  <div
    class="bg-void text-gray-300 font-cormorant min-h-screen flex items-center justify-center relative overflow-hidden selection:bg-blood selection:text-white"
  >
    <div class="noise-overlay"></div>

    <div
      class="absolute inset-0 bg-dark-mist bg-cover bg-center bg-no-repeat opacity-40 mix-blend-luminosity scale-105 z-0"
    ></div>

    <div
      class="absolute inset-0 bg-gradient-to-b from-void via-transparent to-void z-0 opacity-90"
    ></div>

    <div
      class="absolute inset-0 bg-gradient-to-r from-void via-transparent to-void z-0 opacity-90"
    ></div>

    <div
      class="absolute inset-0 bg-[radial-gradient(circle_at_center,transparent_0%,#070709_80%)] z-0"
    ></div>

    <div
      class="obsidian-panel relative z-10 w-full max-w-md p-10 md:p-12 mx-4 rounded border-t-2 border-t-blood shadow-[0_0_30px_rgba(138,3,3,0.1)]"
    >
      <div class="flex justify-center mb-6">
        <svg
          class="w-12 h-12 text-blood animate-pulse-slow"
          viewBox="0 0 24 24"
          fill="none"
          stroke="currentColor"
        >
          <circle
            cx="12"
            cy="10"
            r="7"
            stroke-dasharray="2 2"
          />
          <path d="M12 3v18M8 14h8" stroke-width="1.5" />
          <path
            d="M12 3a3 3 0 1 0 0 6 3 3 0 1 0 0-6z"
            fill="currentColor"
            opacity="0.2"
          />
          <path
            d="M12 21.5c1.5 0 2.5-1 2.5-2.5S12 16 12 16s-2.5 1.5-2.5 3 1 2.5 2.5 2.5z"
            fill="currentColor"
            stroke="none"
          />
        </svg>
      </div>

      <div class="text-center mb-10">
        <h1
          class="font-cinzel text-3xl md:text-4xl font-bold text-white tracking-widest"
        >
          Identifícate
        </h1>

        <p
          class="font-cinzel text-blood mt-2 text-sm md:text-base tracking-[0.2em] uppercase font-bold"
        >
          ante las sombras
        </p>
      </div>

      <form @submit.prevent="iniciarSesion" class="space-y-8">
        <div class="relative group">
          <label
            class="block font-cinzel text-xs text-ash tracking-[0.15em] uppercase mb-2"
          >
            Nombre de Usuario
          </label>

          <input
            v-model="username"
            type="text"
            autocomplete="off"
            class="input-dark w-full bg-[#0a0a0d] border border-gray-800 text-gray-200 px-4 py-3 rounded-sm"
            placeholder="Quien camina en la noche..."
          />
        </div>

        <div class="relative group">
          <label
            class="block font-cinzel text-xs text-ash tracking-[0.15em] uppercase mb-2"
          >
            PIN de Ingreso
          </label>

          <input
            v-model="pin"
            type="password"
            inputmode="numeric"
            class="input-dark w-full bg-[#0a0a0d] border border-gray-800 text-gray-200 px-4 py-3 rounded-sm text-center"
            placeholder="••••"
          />
        </div>

        <button
          type="submit"
          :disabled="cargando"
          class="w-full relative overflow-hidden bg-blood hover:bg-blood-dark text-white font-cinzel font-bold text-xl py-4 rounded-sm uppercase tracking-[0.2em]"
        >
          <span>
            {{ cargando ? 'Abriendo el Velo...' : 'Despierta' }}
          </span>
        </button>
      </form>

      <div class="mt-12 pt-6 border-t border-gray-800/60 text-center">
        <p class="text-ash text-lg italic leading-relaxed">
          "La mascarada debe mantenerse.<br />
          <span class="text-gray-400 font-semibold">
            El apocalipsis se acerca.
          </span>
          "
        </p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router' // 1. Importamos el router
import axios from 'axios'
import Swal from 'sweetalert2'

// 2. Inicializamos el router
const router = useRouter()

const username = ref('')
const pin = ref('')
const cargando = ref(false)

onMounted(() => {
  if (window.tailwind) {
    window.tailwind.config = {
      theme: {
        extend: {
          colors: {
            blood: {
              DEFAULT: '#8a0303',
              dark: '#4a0000',
              glow: 'rgba(138,3,3,0.5)'
            },
            void: '#070709',
            ash: '#8c8c8c'
          },
          fontFamily: {
            cinzel: ['Cinzel', 'serif'],
            cormorant: ['Cormorant Garamond', 'serif']
          },
          backgroundImage: {
            'dark-mist':
              "url('https://images.unsplash.com/photo-1478479405421-ce83c92fb3ba?q=80&w=2000&auto=format&fit=crop')"
          },
          animation: {
            'pulse-slow': 'pulse 4s cubic-bezier(0.4,0,0.6,1) infinite'
          }
        }
      }
    }
  }
})

/* 🔥 Método que se ejecuta si todo sale bien */
const continuarFlujo = (data) => {
  // 3. Guardamos el token en el localStorage (usamos 'Token' con mayúscula como lo definiste antes)
  localStorage.setItem('Token', data.token)
  
  // Opcional: También puedes guardar la expiración si la necesitas para verificar sesiones caducadas después
  localStorage.setItem('TokenExp', data.expiracion)

  // 4. Mostramos el mensaje y redirigimos
  Swal.fire({
    icon: 'success',
    title: 'El Velo se abre',
    text: 'Acceso concedido a las sombras.',
    confirmButtonColor: '#8a0303',
    timer: 1500, // Espera 1.5 segundos para que el usuario lea el mensaje
    showConfirmButton: false // Ocultamos el botón de "OK" para que sea fluido
  }).then(() => {
    // 5. Redireccionamos a la página de Inicio
    // Asegúrate de que '/' es el path correcto para tu Inicio.vue en src/router/index.js
    router.push('/') 
  })
}

/* 💀 LOGIN */
const iniciarSesion = async () => {
  cargando.value = true

  try {
    const login = {
      NombreUsuario: username.value,
      Pin: pin.value,
      tipoUsuario: 1
    }

    const response = await axios.post('https://localhost:7084/api/usuarios', login)

    // ✔️ Si todo OK (200–299)
    continuarFlujo(response.data)

  } catch (error) {
    // ❌ BadRequest del backend
    if (error.response && error.response.status === 400) {
      Swal.fire({
        icon: 'error',
        title: 'Acceso denegado',
        text: error.response.data?.message || 'Credenciales inválidas',
        confirmButtonColor: '#8a0303'
      })
      return
    }

    // ⚠️ otros errores (500, red, etc)
    Swal.fire({
      icon: 'error',
      title: 'El letargo es profundo',
      text: 'Ocurrió un problema inesperado de conexión.',
      confirmButtonColor: '#8a0303'
    })

  } finally {
    cargando.value = false
  }
}
</script>

<style scoped>
/* PEGA AQUÍ TODO EL CSS QUE TENÍAS DENTRO DEL <style> ORIGINAL */


/* Ejemplo */
.noise-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  pointer-events: none;
}

.obsidian-panel {
  background: rgba(7, 7, 9, 0.85);
  backdrop-filter: blur(12px);
}

@keyframes blade-shine {
  0% {
    transform: translateX(-100%) skewX(-15deg);
  }

  100% {
    transform: translateX(200%) skewX(-15deg);
  }
}
</style>