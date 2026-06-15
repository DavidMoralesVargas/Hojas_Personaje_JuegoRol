<template>
  <div class="page-layout">
    <Header />

    <div class="disciplinas-container">
      <header class="header-section">
        <h1 class="blood-title">Gestión de Disciplinas</h1>
        <button class="btn-blood shadow-glow" @click="toggleCreateForm">
          + Nueva Disciplina
        </button>
      </header>

      <div v-if="showCreateForm" class="modal-backdrop" @click.self="toggleCreateForm">
        <div class="modal-content">
          <div class="modal-header">
            <h3>Inscribir Nueva Disciplina</h3>
            <button class="btn-close" @click="toggleCreateForm">✖</button>
          </div>

          <div class="modal-body">
            <div class="form-group">
              <label>Nombre de la Disciplina:</label>
              <input v-model="newDisciplina.nombre" type="text" placeholder="Ej. Dominación..." class="input-dark" />
            </div>

            <div class="habilidades-section">
              <h4>Poderes y Habilidades</h4>
              <div v-for="(hab, index) in newDisciplina.habilidades" :key="index" class="habilidad-card">
                <div class="hab-header">
                  <span class="hab-title">Habilidad #{{ index + 1 }}</span>
                  <button class="btn-danger-icon" @click="removeHabilidad(index)" title="Eliminar Habilidad">✖</button>
                </div>
                <div class="hab-grid">
                  <input v-model="hab.nombre" type="text" placeholder="Nombre" class="input-dark" />
                  <input v-model="hab.nivel" type="number" min="1" max="5" placeholder="Nivel (1-5)" class="input-dark" />
                  <input v-model="hab.tirada" type="text" placeholder="Tirada (Opcional)" class="input-dark" />
                  <label class="checkbox-label">
                    <input v-model="hab.enardecimiento" type="checkbox" />
                    ¿Requiere Enardecimiento?
                  </label>
                </div>
              </div>
              <button class="btn-secondary w-100" @click="addHabilidad">+ Añadir Habilidad</button>
            </div>
          </div>

          <div class="modal-footer">
            <button class="btn-blood w-100" @click="guardarDisciplina">Guardar Disciplina Completa</button>
          </div>
        </div>
      </div>

      <div class="table-wrapper">
        <table class="vampire-table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Nombre</th>
              <th>Poderes</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="d in disciplinas" :key="d.id">
              <td data-label="ID">{{ d.id }}</td>
              
              <td data-label="Nombre" class="name-cell">
                <div v-if="editingId === d.id">
                  <input v-model="editData.nombre" type="text" class="input-dark w-100" />
                </div>
                <div v-else>
                  {{ d.nombre }}
                </div>
              </td>

              <td data-label="Poderes">
                <button 
                  class="btn-id-link" 
                  title="Ver detalles de habilidades"
                  @click="irADetalle(d.id)"
                >
                  {{ d.habilidadDisciplinas ? d.habilidadDisciplinas.length : 0 }}
                </button>
              </td>

              <td data-label="Acciones">
                <div v-if="editingId === d.id" class="action-buttons">
                  <button class="btn-blood btn-sm" @click="guardarEdicion(d.id)">Guardar</button>
                  <button class="btn-secondary btn-sm" @click="cancelarEdicion">Cancelar</button>
                </div>
                <div v-else class="action-buttons">
                  <button class="btn-secondary btn-sm" @click="iniciarEdicion(d)">Editar</button>
                  <button class="btn-danger btn-sm" @click="eliminarDisciplina(d.id)">Eliminar</button>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
// Importamos el Header asegurando la ruta correcta a tu carpeta de componentes
import Header from '../../components/Header.vue'; 
import { api } from '../../services/api'; 
import Swal from 'sweetalert2';

const router = useRouter();
const disciplinas = ref([]);

// --- ESTADO PARA CREACIÓN ---
const showCreateForm = ref(false);
const newDisciplina = ref({
  nombre: '',
  habilidades: []
});

// --- ESTADO PARA EDICIÓN ---
const editingId = ref(null);
const editData = ref({
  id: 0,
  nombre: ''
});

// --- MÉTODOS DE CICLO DE VIDA ---
onMounted(() => {
  cargarDisciplinas();
});

// --- MÉTODOS DE API ---
const cargarDisciplinas = async () => {
  try {
    const data = await api.get('/api/Disciplinas');
    disciplinas.value = data;
  } catch (error) {
    console.error("Error al cargar disciplinas", error);
  }
};

const guardarDisciplina = async () => {
  if (!newDisciplina.value.nombre) {
    Swal.fire({ icon: 'warning', title: 'Faltan datos', text: 'El nombre es obligatorio.', background: '#1c1c1e', color: '#d4d4d4' });
    return;
  }
  
  try {
    await api.post('/api/Disciplinas', newDisciplina.value);
    Swal.fire({ icon: 'success', title: 'Éxito', text: 'Disciplina documentada en los registros.', background: '#1c1c1e', color: '#d4d4d4', confirmButtonColor: '#8a0303' });
    
    newDisciplina.value = { nombre: '', habilidades: [] };
    showCreateForm.value = false;
    cargarDisciplinas();
  } catch (error) {
    console.error("Error al guardar disciplina", error);
  }
};

const eliminarDisciplina = async (id) => {
  const result = await Swal.fire({
    title: '¿Muerte Definitiva?',
    text: "Esta acción destruirá la disciplina y sus registros permanentemente.",
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#8a0303',
    cancelButtonColor: '#333',
    confirmButtonText: 'Sí, borrarla',
    cancelButtonText: 'Cancelar',
    background: '#1c1c1e',
    color: '#d4d4d4'
  });

  if (result.isConfirmed) {
    try {
      await api.remove(`/api/Disciplinas/${id}`);
      cargarDisciplinas();
    } catch (error) {
      console.error("Error al eliminar", error);
    }
  }
};

const guardarEdicion = async (id) => {
  try {
    await api.put('/api/Disciplinas', {
      id: editData.value.id,
      nombre: editData.value.nombre
    });
    editingId.value = null;
    cargarDisciplinas();
  } catch (error) {
    console.error("Error al editar", error);
  }
};

// --- MÉTODOS DE INTERFAZ ---
const toggleCreateForm = () => {
  showCreateForm.value = !showCreateForm.value;
  if (!showCreateForm.value) {
    newDisciplina.value = { nombre: '', habilidades: [] };
  }
};

const addHabilidad = () => {
  newDisciplina.value.habilidades.push({
    nombre: '',
    nivel: 1,
    enardecimiento: false,
    tirada: ''
  });
};

const removeHabilidad = (index) => {
  newDisciplina.value.habilidades.splice(index, 1);
};

const iniciarEdicion = (disciplina) => {
  editingId.value = disciplina.id;
  editData.value = { id: disciplina.id, nombre: disciplina.nombre };
};

const cancelarEdicion = () => {
  editingId.value = null;
};

const irADetalle = (id) => {
  router.push(`/disciplinas/${id}`);
};
</script>

<style scoped>
/* ==========================================
   VARIABLES Y CONTENEDOR PRINCIPAL
   ========================================== */
.page-layout {
  min-height: 100vh;
  background-color: #121212;
  font-family: 'Georgia', serif;
}

.disciplinas-container {
  color: #d4d4d4;
  padding: 30px; /* Aumentado para dar más aire alrededor */
  max-width: 1500px; /* Aumentado de 1200 a 1500px para pantallas más grandes */
  margin: 0 auto;
}

.header-section {
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-bottom: 2px solid #333;
  padding-bottom: 20px;
  margin-bottom: 30px; /* Más espacio debajo del título */
  flex-wrap: wrap;
  gap: 15px;
}

.blood-title {
  color: #8a0303;
  margin: 0;
  font-size: 2.5rem; /* Título ligeramente más grande */
}

/* ==========================================
   BOTONES E INPUTS
   ========================================== */
.btn-blood {
  background-color: #8a0303;
  color: white;
  border: none;
  padding: 12px 24px; /* Botones más grandes */
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

.btn-sm { padding: 8px 16px; font-size: 1rem; }
.btn-danger-icon {
  background: transparent;
  color: #ff4d4d;
  border: none;
  font-size: 1.2rem;
  cursor: pointer;
}
.btn-close {
  background: transparent;
  color: #d4d4d4;
  border: none;
  font-size: 1.5rem;
  cursor: pointer;
}
.btn-close:hover { color: #fff; }

.input-dark {
  background-color: #2c2c2e;
  color: #fff;
  border: 1px solid #555;
  padding: 12px;
  font-size: 1.05rem; /* Inputs de texto más grandes */
  border-radius: 4px;
  width: 100%;
  box-sizing: border-box;
}
.input-dark:focus {
  outline: none;
  border-color: #8a0303;
}
.w-100 { width: 100%; margin-top: 10px; }

/* ==========================================
   MODAL DE CREACIÓN
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
  max-width: 700px; /* Modal ligeramente más ancho */
  max-height: 90vh;
  display: flex;
  flex-direction: column;
  box-shadow: 0 5px 25px rgba(0,0,0,0.8);
}
.modal-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 20px 25px;
  border-bottom: 1px solid #333;
}
.modal-header h3 { margin: 0; color: #8a0303; font-size: 1.5rem; }
.modal-body {
  padding: 25px;
  overflow-y: auto;
}
.modal-footer {
  padding: 20px 25px;
  border-top: 1px solid #333;
}

.habilidad-card {
  background-color: #252527;
  border: 1px solid #444;
  padding: 20px;
  border-radius: 6px;
  margin-bottom: 15px;
}
.hab-header {
  display: flex;
  justify-content: space-between;
  margin-bottom: 15px;
  border-bottom: 1px dashed #555;
  padding-bottom: 10px;
}
.hab-title { font-weight: bold; color: #aaa; font-size: 1.1rem; }
.hab-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 15px;
}
.checkbox-label {
  display: flex;
  align-items: center;
  gap: 10px;
  color: #ccc;
  font-size: 1.05rem;
}

/* ==========================================
   TABLA Y RESPONSIVE (AMPLIADO)
   ========================================== */
.table-wrapper {
  overflow-x: auto;
  background-color: #1c1c1e;
  border: 1px solid #333;
  border-radius: 8px;
}
.vampire-table {
  width: 100%;
  border-collapse: collapse;
  min-width: 800px; /* Aumentado para forzar un mejor uso del espacio */
}
.vampire-table th, .vampire-table td {
  border-bottom: 1px solid #333;
  padding: 22px 25px; /* Celdas mucho más espaciosas */
  text-align: left;
  font-size: 1.15rem; /* Texto más grande en toda la tabla */
}
.vampire-table th {
  background-color: #121212;
  color: #8a0303;
  font-weight: bold;
  text-transform: uppercase;
  font-size: 1.05rem; /* Cabeceras destacadas */
  letter-spacing: 2px;
}
.vampire-table tr:hover { background-color: #252527; }

.btn-id-link {
  background-color: #2c2c2e;
  color: #d4d4d4;
  border: 1px solid #8a0303;
  border-radius: 5px;
  min-width: 50px;
  height: 45px; /* Botón de conteo más grande */
  font-weight: bold;
  font-size: 1.2rem; /* Número más visible */
  cursor: pointer;
  transition: all 0.3s;
}
.btn-id-link:hover {
  background-color: #8a0303;
  color: #fff;
  box-shadow: 0 0 8px #8a0303;
}
.action-buttons {
  display: flex;
  gap: 10px; /* Más separación entre botones de acción */
}

/* ==========================================
   MEDIA QUERIES (MÓVILES)
   ========================================== */
@media (max-width: 768px) {
  .header-section {
    flex-direction: column;
    align-items: flex-start;
  }
  .hab-grid {
    grid-template-columns: 1fr; 
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
    font-size: 1.1rem; /* Mantenemos buena lectura en móviles */
    padding-top: 15px;
    padding-bottom: 15px;
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
  .name-cell {
    display: flex;
    justify-content: flex-end;
  }
}
</style>