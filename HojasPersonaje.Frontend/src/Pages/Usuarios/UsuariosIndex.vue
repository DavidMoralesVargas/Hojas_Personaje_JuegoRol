<template>
    <Header/>
  <div class="vtm-container">
    <header class="vtm-header">
      <h1>Gestión de la Camarilla</h1>
      <p>Administración de Usuarios y Vástagos</p>
    </header>

    <div v-if="loading" class="vtm-alert info">Invocando registros...</div>

    <div v-if="!loading" class="table-responsive">
      <table class="vtm-table">
        <thead>
          <tr>
            <th>Foto</th>
            <th>Nombre de Usuario</th>
            <th>Rango / Tipo</th>
            <th class="text-center">Acciones</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="usuario in usuarios" :key="usuario.id">
            <td>
              <img 
                :src="usuario.foto || defaultAvatar" 
                alt="Avatar" 
                class="vtm-avatar"
                @error="onImageError"
              />
            </td>
            <td class="username">{{ usuario.nombre_Usuario }}</td>
            <td>
              <span class="vtm-badge" :class="getRoleClass(usuario.tipoUsuario)">
                {{ getRoleName(usuario.tipoUsuario) }}
              </span>
            </td>
            <td class="text-center">
              <div class="btn-group">
                <button @click="abrirEditar(usuario)" class="btn btn-edit" title="Editar Vástago">
                  <span>Modificar</span>
                </button>
                <button @click="eliminarUsuario(usuario)" class="btn btn-delete" title="Eliminar de la lista">
                  <span>Eliminar</span>
                </button>
              </div>
            </td>
          </tr>
          <tr v-if="usuarios.length === 0">
            <td colspan="4" class="text-center empty-msg">No hay usuarios registrados en este clan.</td>
          </tr>
        </tbody>
      </table>
    </div>

    <div v-if="mostrarModal" class="vtm-modal-overlay" @click.self="cerrarModal">
      <div class="vtm-modal">
        <div class="modal-header">
          <h3>Modificar Perfil</h3>
          <button @click="cerrarModal" class="close-btn">&times;</button>
        </div>
        
        <form @submit.prevent="guardarCambios">
          <div class="form-group">
            <label for="edit-username">Nombre de Usuario</label>
            <input 
              id="edit-username"
              type="text" 
              v-model="usuarioEditando.NombreUsuario" 
              required
              class="vtm-input"
            />
          </div>

          <div class="form-group">
            <label for="edit-pin">Contraseña (PIN)</label>
            <input 
                id="edit-pin"
                type="password" 
                v-model="usuarioEditando.Pin" 
                class="vtm-input"
                placeholder="••••••••"
            />
            <small class="form-help">Deja los puntos si no deseas cambiar la contraseña.</small>
          </div>

          <div class="modal-actions">
            <button type="button" @click="cerrarModal" class="btn btn-secondary">Cancelar</button>
            <button type="submit" class="btn btn-save" :disabled="guardando">
              {{ guardando ? 'Guardando...' : 'Guardar Cambios' }}
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { api } from '../../services/api'; 
import Swal from 'sweetalert2';
import Header from '../../components/Header.vue';

const ENDPOINT = '/api/Usuarios';

// Estados
const usuarios = ref([]);
const loading = ref(false);
const guardando = ref(false);
const mostrarModal = ref(false);

const defaultAvatar = `data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="%23555"><path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 3c1.66 0 3 1.34 3 3s-1.34 3-3 3-3-1.34-3-3 1.34-3 3-3zm0 14.2c-2.5 0-4.71-1.28-6-3.22.03-1.99 4-3.08 6-3.08 1.99 0 5.97 1.09 6 3.08-1.29 1.94-3.5 3.22-6 3.22z"/></svg>`;

const usuarioEditando = ref({
  id: 0,
  NombreUsuario: '',
  Pin: '',
  tipoUsuario: 0
});

const cargarUsuarios = async () => {
  loading.value = true;
  try {
    usuarios.value = await api.get(ENDPOINT);
  } catch (err) {
    console.error("Error al invocar los registros:", err);
  } finally {
    loading.value = false;
  }
};

const eliminarUsuario = async (usuario) => {
  const resultadoConfirmacion = await Swal.fire({
    title: '¿Proceder con el Destierro?',
    text: `¿Estás seguro de que deseas eliminar a ${usuario.nombre_Usuario} de los registros?`,
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#8a0303',
    cancelButtonColor: '#333',
    confirmButtonText: 'Sí, borrar',
    cancelButtonText: 'Cancelar',
    background: '#1c1c1e',
    color: '#d4d4d4'
  });

  if (!resultadoConfirmacion.isConfirmed) return;

  try {
    await api.remove(`${ENDPOINT}/${usuario.id}`);
    usuarios.value = usuarios.value.filter(u => u.id !== usuario.id);
    
    Swal.fire({
      icon: 'success',
      title: 'Hecho',
      text: 'El registro ha sido eliminado con éxito.',
      confirmButtonColor: '#8a0303',
      background: '#1c1c1e',
      color: '#d4d4d4'
    });
  } catch (err) {
    console.error("No se pudo desterrar al usuario:", err);
  }
};

const abrirEditar = (usuario) => {
  usuarioEditando.value = {
    id: usuario.id,
    NombreUsuario: usuario.nombre_Usuario,
    Pin: '', // <-- INICIA VACÍO: El placeholder "••••••••" del HTML hará el trabajo visual
    tipoUsuario: usuario.tipoUsuario
  };
  mostrarModal.value = true;
};

const cerrarModal = () => {
  mostrarModal.value = false;
};

const guardarCambios = async () => {
  guardando.value = true;
  try {
    const datosEnviar = { ...usuarioEditando.value };
    
    // Ya no es necesario limpiar los puntos simulados porque el input arrancó vacío.
    // Si el usuario no escribió nada, "Pin" viajará como un string vacío ('') y el backend lo ignorará.

    await api.put(ENDPOINT, datosEnviar);

    const index = usuarios.value.findIndex(u => u.id === datosEnviar.id);
    if (index !== -1) {
      usuarios.value[index].nombre_Usuario = datosEnviar.NombreUsuario;
    }

    cerrarModal();

    Swal.fire({
      icon: 'success',
      title: 'Registros Actualizados',
      text: 'Los cambios se han guardado con éxito en el libro del clan.',
      confirmButtonColor: '#8a0303',
      background: '#1c1c1e',
      color: '#d4d4d4'
    });
  } catch (err) {
    console.error("Error al actualizar cambios:", err);
  } finally {
    guardando.value = false;
  }
};

const getRoleName = (tipo) => {
  if (tipo === 0) return 'Administrador';
  if (tipo === 1) return 'Jugador';
  return 'Jugador';
};

const getRoleClass = (tipo) => {
  if (tipo === 0) return 'badge-admin';
  if (tipo === 1) return 'badge-jugador';
  return 'badge-jugador';
};

const onImageError = (e) => {
  e.target.src = defaultAvatar;
};

onMounted(() => {
  cargarUsuarios();
});
</script>

<style scoped>
/* --- ESTILO VAMPIRO LA MASCARADA (DARK ELEGANT) --- */
@import url('https://fonts.googleapis.com/css2?family=Cinzel:wght@500;700&family=Inter:wght@300;400;600&display=swap');

.vtm-container {
  background-color: #121212; 
  color: #e0e0e0; 
  font-family: 'Inter', sans-serif;
  padding: 2rem;
  min-height: 100vh;
}

.vtm-header {
  border-bottom: 2px solid #8b0000; 
  padding-bottom: 1rem;
  margin-bottom: 2rem;
  text-align: left;
}

.vtm-header h1 {
  font-family: 'Cinzel', serif; 
  color: #ffffff;
  font-size: 2.5rem;
  letter-spacing: 2px;
  margin: 0;
  text-shadow: 0 0 10px rgba(139, 0, 0, 0.6);
}

.vtm-header p {
  font-style: italic;
  color: #888;
  margin: 0.5rem 0 0 0;
}

.table-responsive {
  width: 100%;
  overflow-x: auto;
  background: #1a1a1a;
  border-radius: 8px;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.5);
  border: 1px solid #2d2d2d;
}

.vtm-table {
  width: 100%;
  border-collapse: collapse;
  text-align: left;
}

.vtm-table th {
  background-color: #0f0f0f;
  color: #b392ac; 
  font-family: 'Cinzel', serif;
  font-weight: 600;
  padding: 1rem;
  border-bottom: 2px solid #3a0000;
  letter-spacing: 1px;
}

.vtm-table td {
  padding: 1rem;
  border-bottom: 1px solid #252525;
  vertical-align: middle;
}

.vtm-table tbody tr:hover {
  background-color: #222222; 
}

.username {
  font-weight: 600;
  color: #ffffff;
}

.vtm-avatar {
  width: 45px;
  height: 45px;
  border-radius: 50%;
  object-fit: cover;
  border: 2px solid #8b0000;
  background-color: #111;
}

.vtm-badge {
  padding: 0.25rem 0.6rem;
  border-radius: 4px;
  font-size: 0.8rem;
  font-weight: bold;
  text-transform: uppercase;
}
.badge-admin { background: rgba(139, 0, 0, 0.2); color: #ff4d4d; border: 1px solid #8b0000; }
.badge-narrador { background: rgba(212, 175, 55, 0.1); color: #d4af37; border: 1px solid #d4af37; }
.badge-jugador { background: rgba(212, 175, 55, 0.1); color: #06c3fd; border: 1px solid #0c80ec; }
/*.badge-jugador { background: rgba(85, 85, 85, 0.2); color: #ccc; border: 1px solid #555; } */

.btn-group {
  display: flex;
  gap: 0.5rem;
  justify-content: center;
}

.btn {
  padding: 0.5rem 1rem;
  border: none;
  border-radius: 4px;
  font-size: 0.85rem;
  cursor: pointer;
  font-weight: 600;
  transition: all 0.3s ease;
}

.btn-edit {
  background-color: #2b2b2b;
  color: #e0e0e0;
  border: 1px solid #444;
}
.btn-edit:hover {
  background-color: #3d3d3d;
  color: #fff;
  border-color: #666;
}

.btn-delete {
  background-color: #4a0000;
  color: #ff9999;
  border: 1px solid #6a0000;
}
.btn-delete:hover {
  background-color: #8b0000;
  color: #ffffff;
  box-shadow: 0 0 8px rgba(139, 0, 0, 0.8);
}

.vtm-alert {
  padding: 1rem;
  border-radius: 4px;
  margin-bottom: 1rem;
}
.vtm-alert.info { background: #181c24; border-left: 4px solid #4a90e2; color: #cee; }

.vtm-modal-overlay {
  position: fixed;
  top: 0; left: 0; width: 100%; height: 100%;
  background: rgba(0, 0, 0, 0.85);
  display: flex; justify-content: center; align-items: center;
  z-index: 1000;
  backdrop-filter: blur(3px);
}

.vtm-modal {
  background: #1a1a1a;
  border: 1px solid #8b0000;
  border-radius: 8px;
  width: 450px;
  max-width: 90%;
  padding: 1.5rem;
  box-shadow: 0 10px 30px rgba(0,0,0,0.7);
  animation: fadeIn 0.2s ease-out;
}

.modal-header {
  display: flex; justify-content: space-between; align-items: center;
  border-bottom: 1px solid #333;
  padding-bottom: 0.75rem;
  margin-bottom: 1.5rem;
}

.modal-header h3 {
  font-family: 'Cinzel', serif;
  color: #ffffff;
  margin: 0;
}

.close-btn {
  background: none; border: none; color: #888; font-size: 1.5rem; cursor: pointer;
}
.close-btn:hover { color: #8b0000; }

.form-group {
  margin-bottom: 1.25rem;
  display: flex; flex-direction: column;
}

.form-group label {
  font-size: 0.9rem; color: #aaa; margin-bottom: 0.5rem; text-transform: uppercase; letter-spacing: 1px;
}

.vtm-input {
  background: #0d0d0d;
  border: 1px solid #333;
  color: #fff;
  padding: 0.75rem;
  border-radius: 4px;
  font-size: 1rem;
}
.vtm-input:focus {
  outline: none; border-color: #8b0000; box-shadow: 0 0 5px rgba(139, 0, 0, 0.5);
}

.form-help { color: #666; font-size: 0.8rem; margin-top: 0.25rem; }

.modal-actions {
  display: flex; justify-content: flex-end; gap: 0.75rem; margin-top: 2rem;
}

.btn-secondary { background: #333; color: #ccc; }
.btn-secondary:hover { background: #444; color: #fff; }

.btn-save { background: #8b0000; color: #fff; }
.btn-save:hover:not(:disabled) { background: #b30000; box-shadow: 0 0 10px rgba(179, 0, 0, 0.6); }
.btn-save:disabled { opacity: 0.5; cursor: not-allowed; }

.text-center { text-align: center;}
.empty-msg { color: #666; font-style: italic; }

@keyframes fadeIn {
  from { opacity: 0; transform: scale(0.95); }
  to { opacity: 1; transform: scale(1); }
}
</style>