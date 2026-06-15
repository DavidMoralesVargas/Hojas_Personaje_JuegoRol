Tienes toda la razón. El error radica en que el endpoint /api/HabilidadDisciplina/all/{id} devuelve directamente una lista (array) de habilidades, pero la plantilla HTML estaba intentando leer un objeto anidado (disciplina.habilidades). Por eso la tabla se quedaba en las sombras y no mostraba nada.

Para solucionarlo, he separado los registros: ahora consultamos la Disciplina (para obtener su nombre y mostrarlo en el título) y consultamos las Habilidades (usando tu endpoint all/{id}) guardándolas en su propia lista independiente para iterar sobre ella correctamente.

Aquí tienes el archivo corregido. Puedes reemplazarlo por completo:

HTML
<template>
  <div class="page-layout">
    <Header />

    <div class="disciplina-detail-container">
      <div class="header-actions">
        <button class="btn-secondary" @click="volverAlListado">← Volver al Listado</button>
      </div>
      
      <h1 class="blood-title">Disciplina: {{ disciplina.nombre || 'Cargando...' }}</h1>
      
      <div class="panel">
        <div class="panel-header">
          <h3>Habilidades Conocidas</h3>
          <button class="btn-blood shadow-glow" @click="abrirModalCrear">
            + Añadir Habilidad
          </button>
        </div>
        
        <div class="table-wrapper">
          <table class="vampire-table" v-if="habilidades.length > 0">
            <thead>
              <tr>
                <th>Nivel</th>
                <th>Nombre</th>
                <th>Tirada</th>
                <th>Enardecimiento</th>
                <th>Acciones</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="hab in habilidades" :key="hab.id">
                <td data-label="Nivel">{{ hab.nivel }}</td>
                <td data-label="Nombre"><strong>{{ hab.nombre }}</strong></td>
                <td data-label="Tirada">{{ hab.tirada || 'Automático' }}</td>
                <td data-label="Enardecimiento">
                  <span v-if="hab.enardecimiento" class="text-blood">Sí (Rouse Check)</span>
                  <span v-else>No</span>
                </td>
                <td data-label="Acciones" class="action-buttons">
                  <button class="btn-secondary btn-sm" @click="abrirModalEditar(hab)">Editar</button>
                  <button class="btn-danger btn-sm" @click="eliminarHabilidad(hab.id)">Eliminar</button>
                </td>
              </tr>
            </tbody>
          </table>
          <p v-else class="empty-state">No hay habilidades registradas en los tomos para esta disciplina.</p>
        </div>
      </div>
    </div>

    <div v-if="showModal" class="modal-backdrop" @click.self="cerrarModal">
      <div class="modal-content">
        <div class="modal-header">
          <h3>{{ isEditing ? 'Editar Habilidad' : 'Inscribir Nueva Habilidad' }}</h3>
          <button class="btn-close" @click="cerrarModal">✖</button>
        </div>

        <div class="modal-body form-grid">
          <div class="form-group full-width">
            <label>Nombre del Poder:</label>
            <input v-model="formHabilidad.nombre" type="text" placeholder="Ej. Compulsión..." class="input-dark" />
          </div>
          
          <div class="form-group">
            <label>Nivel (1-5):</label>
            <input v-model="formHabilidad.nivel" type="number" min="1" max="5" class="input-dark" />
          </div>

          <div class="form-group">
            <label>Tirada (Opcional):</label>
            <input v-model="formHabilidad.tirada" type="text" placeholder="Ej. Carisma + Intimidación" class="input-dark" />
          </div>

          <div class="form-group full-width checkbox-container">
            <label class="checkbox-label">
              <input v-model="formHabilidad.enardecimiento" type="checkbox" />
              ¿Requiere chequeo de Enardecimiento (Rouse Check)?
            </label>
          </div>
        </div>

        <div class="modal-footer">
          <button class="btn-secondary" @click="cerrarModal">Cancelar</button>
          <button class="btn-blood" @click="guardarHabilidad">
            {{ isEditing ? 'Guardar Cambios' : 'Añadir al Tomo' }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import Header from '../../components/Header.vue'; 
import { api } from '../../services/api';
import Swal from 'sweetalert2';

const route = useRoute();
const router = useRouter();
const disciplinaId = ref(parseInt(route.params.id));

// Separamos el estado: un objeto para la disciplina y un array para la lista
const disciplina = ref({ nombre: '' });
const habilidades = ref([]);

// Estados para el Modal
const showModal = ref(false);
const isEditing = ref(false);
const formHabilidad = ref({
  id: 0,
  nombre: '',
  nivel: 1,
  enardecimiento: false,
  tirada: '',
  disciplinaId: 0
});

onMounted(() => {
  cargarDatosDisciplina();
  cargarHabilidades();
});

// 1. Obtiene el nombre de la Disciplina para la cabecera
const cargarDatosDisciplina = async () => {
  try {
    const data = await api.get(`/api/Disciplinas/${disciplinaId.value}`);
    disciplina.value = data;
  } catch (error) {
    console.error("Error al obtener la disciplina base", error);
  }
};

// 2. Obtiene la lista estricta de Habilidades usando tu endpoint específico
const cargarHabilidades = async () => {
  try {
    const data = await api.get(`/api/HabilidadDisciplina/all/${disciplinaId.value}`);
    // Aseguramos que se guarde en el array que lee la tabla
    habilidades.value = data || []; 
  } catch (error) {
    console.error("Error al obtener la lista de habilidades", error);
    Swal.fire({ icon: 'error', title: 'Error', text: 'No se pudieron leer los poderes del tomo.', background: '#1c1c1e', color: '#d4d4d4' });
  }
};

const volverAlListado = () => {
  router.push('/disciplinas');
};

// --- LOGICA DEL MODAL ---
const abrirModalCrear = () => {
  isEditing.value = false;
  formHabilidad.value = {
    id: 0,
    nombre: '',
    nivel: 1,
    enardecimiento: false,
    tirada: '',
    disciplinaId: disciplinaId.value // Asociado a la disciplina actual
  };
  showModal.value = true;
};

const abrirModalEditar = (hab) => {
  isEditing.value = true;
  formHabilidad.value = {
    id: hab.id,
    nombre: hab.nombre,
    nivel: hab.nivel,
    enardecimiento: hab.enardecimiento,
    tirada: hab.tirada,
    disciplinaId: hab.disciplinaId || disciplinaId.value
  };
  showModal.value = true;
};

const cerrarModal = () => {
  showModal.value = false;
};

// --- LLAMADAS AL CONTROLADOR (CRUD) ---
const guardarHabilidad = async () => {
  if (!formHabilidad.value.nombre || formHabilidad.value.nivel < 1) {
    Swal.fire({ icon: 'warning', title: 'Datos Insuficientes', text: 'El nombre y nivel son obligatorios.', background: '#1c1c1e', color: '#d4d4d4' });
    return;
  }

  try {
    if (isEditing.value) {
      await api.put('/api/HabilidadDisciplina', formHabilidad.value);
      Swal.fire({ icon: 'success', title: 'Modificada', text: 'La habilidad ha sido actualizada.', timer: 1500, showConfirmButton: false, background: '#1c1c1e', color: '#d4d4d4' });
    } else {
      await api.post('/api/HabilidadDisciplina', formHabilidad.value);
      Swal.fire({ icon: 'success', title: 'Añadida', text: 'Nueva habilidad documentada con éxito.', timer: 1500, showConfirmButton: false, background: '#1c1c1e', color: '#d4d4d4' });
    }
    cerrarModal();
    cargarHabilidades(); // Recargamos solo la lista de habilidades
  } catch (error) {
    console.error("Error al guardar habilidad", error);
    Swal.fire({ icon: 'error', title: 'Error', text: 'Hubo una falla al procesar la solicitud.', background: '#1c1c1e', color: '#d4d4d4' });
  }
};

const eliminarHabilidad = async (id) => {
  const result = await Swal.fire({
    title: '¿Olvidar Poder?',
    text: "El conocimiento de este poder se perderá permanentemente.",
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#8a0303',
    cancelButtonColor: '#333',
    confirmButtonText: 'Sí, erradicarlo',
    cancelButtonText: 'Cancelar',
    background: '#1c1c1e',
    color: '#d4d4d4'
  });

  if (result.isConfirmed) {
    try {
      await api.remove(`/api/HabilidadDisciplina/${id}`);
      cargarHabilidades(); // Recargamos la lista tras eliminar
      Swal.fire({ icon: 'success', title: 'Eliminada', text: 'El poder ha sido purgado.', timer: 1500, showConfirmButton: false, background: '#1c1c1e', color: '#d4d4d4' });
    } catch (error) {
      console.error("Error al eliminar", error);
    }
  }
};
</script>

<style scoped>
/* ==========================================
   LAYOUT PRINCIPAL Y CONTENEDORES
   ========================================== */
.page-layout {
  min-height: 100vh;
  background-color: #121212;
  font-family: 'Georgia', serif;
}

.disciplina-detail-container {
  color: #d4d4d4;
  padding: 30px;
  max-width: 1500px;
  margin: 0 auto;
}

.header-actions {
  margin-bottom: 20px;
}

.blood-title {
  color: #8a0303;
  border-bottom: 2px solid #333;
  padding-bottom: 15px;
  margin-bottom: 30px;
  font-size: 2.5rem;
}

.panel {
  background-color: #1c1c1e;
  padding: 30px;
  border: 1px solid #333;
  border-radius: 8px;
}

.panel-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
  flex-wrap: wrap;
  gap: 15px;
}

.panel-header h3 {
  margin: 0;
  font-size: 1.8rem;
  color: #d4d4d4;
}

.text-blood { color: #8a0303; font-weight: bold; }
.loading-state, .empty-state {
  text-align: center;
  font-size: 1.2rem;
  color: #aaa;
  padding: 40px;
}

/* ==========================================
   BOTONES E INPUTS
   ========================================== */
.btn-blood {
  background-color: #8a0303;
  color: white;
  border: none;
  padding: 12px 24px;
  font-size: 1.05rem;
  cursor: pointer;
  font-weight: bold;
  border-radius: 4px;
  transition: background 0.3s;
}
.btn-blood:hover { background-color: #a80505; }
.shadow-glow { box-shadow: 0 0 10px rgba(138, 3, 3, 0.4); }

.btn-secondary {
  background-color: #3a3a3c;
  color: white;
  border: none;
  padding: 10px 20px;
  font-size: 1rem;
  cursor: pointer;
  border-radius: 4px;
  transition: background 0.3s;
}
.btn-secondary:hover { background-color: #555; }

.btn-danger {
  background-color: #5a0000;
  color: white;
  border: none;
  padding: 10px 20px;
  font-size: 1rem;
  cursor: pointer;
  border-radius: 4px;
  transition: background 0.3s;
}
.btn-danger:hover { background-color: #7a0000; }
.btn-sm { padding: 8px 16px; font-size: 0.95rem; }

/* ==========================================
   TABLA RESPONSIVA Y EXPANDIDA
   ========================================== */
.table-wrapper {
  overflow-x: auto;
  border: 1px solid #333;
  border-radius: 8px;
  background-color: #121212;
}

.vampire-table {
  width: 100%;
  border-collapse: collapse;
  min-width: 800px;
}

.vampire-table th, .vampire-table td {
  border-bottom: 1px solid #333;
  padding: 20px 25px;
  text-align: left;
  font-size: 1.1rem;
}

.vampire-table th {
  background-color: #121212;
  color: #8a0303;
  font-weight: bold;
  text-transform: uppercase;
  letter-spacing: 1px;
}

.vampire-table tr:hover { background-color: #252527; }

.action-buttons {
  display: flex;
  gap: 10px;
}

/* ==========================================
   MODAL DE SANGRE (SOPORTE PARA GRID)
   ========================================== */
.modal-backdrop {
  position: fixed;
  top: 0; left: 0; right: 0; bottom: 0;
  background-color: rgba(0, 0, 0, 0.85);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
  padding: 15px;
}
.modal-content {
  background-color: #1c1c1e;
  border: 1px solid #333;
  border-radius: 8px;
  width: 100%;
  max-width: 500px;
  box-shadow: 0 5px 25px rgba(0,0,0,0.8);
}
.modal-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 20px 25px;
  border-bottom: 1px solid #333;
}
.modal-header h3 { margin: 0; color: #8a0303; font-size: 1.4rem; }
.btn-close {
  background: transparent;
  color: #d4d4d4;
  border: none;
  font-size: 1.5rem;
  cursor: pointer;
}
.btn-close:hover { color: #fff; }

.modal-body {
  padding: 25px;
}
.form-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 15px;
}
.full-width {
  grid-column: span 2;
}
.form-group label {
  display: block;
  margin-bottom: 8px;
  color: #aaa;
  font-size: 0.95rem;
}
.input-dark {
  background-color: #2c2c2e;
  color: #fff;
  border: 1px solid #555;
  padding: 12px;
  font-size: 1.05rem;
  border-radius: 4px;
  width: 100%;
  box-sizing: border-box;
}
.input-dark:focus {
  outline: none;
  border-color: #8a0303;
}
.checkbox-container {
  margin-top: 10px;
}
.checkbox-label {
  display: flex;
  align-items: center;
  gap: 10px;
  color: #d4d4d4 !important;
  font-size: 1.05rem !important;
  cursor: pointer;
}
.modal-footer {
  padding: 20px 25px;
  border-top: 1px solid #333;
  display: flex;
  justify-content: flex-end;
  gap: 10px;
}

/* ==========================================
   MEDIA QUERIES PARA MÓVILES
   ========================================== */
@media (max-width: 768px) {
  .panel-header {
    flex-direction: column;
    align-items: flex-start;
  }
  
  .form-grid {
    grid-template-columns: 1fr;
  }
  .full-width {
    grid-column: span 1;
  }

  .vampire-table thead { display: none; }
  .vampire-table, .vampire-table tbody, .vampire-table tr, .vampire-table td {
    display: block;
    width: 100%;
  }
  .vampire-table tr {
    margin-bottom: 20px;
    border: 1px solid #333;
    border-radius: 6px;
    background-color: #1c1c1e;
  }
  .vampire-table td {
    text-align: right;
    padding-left: 50%;
    position: relative;
    border-bottom: 1px solid #2a2a2c;
  }
  .vampire-table td:last-child { border-bottom: 0; }
  
  .vampire-table td::before {
    content: attr(data-label);
    position: absolute;
    left: 15px;
    width: 45%;
    text-align: left;
    font-weight: bold;
    color: #8a0303;
  }
  .action-buttons {
    justify-content: flex-end;
  }
}
</style>