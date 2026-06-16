<template>
    <Header />
  <div class="vampire-container">
    <div class="header-actions">
      <h2>Vástagos Registrados</h2>
      <button class="btn-blood" @click="irCrear">Abrazar Nuevo Vampiro</button>
    </div>

    <div class="table-responsive">
      <table class="vampire-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Nombre</th>
            <th>Bane (Debilidad)</th>
            <th>Compulsión</th>
            <th class="text-center">N° Disciplinas</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="vampiro in vampiros" :key="vampiro.id">
            <td>{{ vampiro.id }}</td>
            <td>{{ vampiro.nombre }}</td>
            <td>{{ vampiro.debilidadesClanes?.[0]?.bane || 'Sin debilidad' }}</td>
            <td>{{ vampiro.debilidadesClanes?.[0]?.compulsion || 'Sin compulsión' }}</td>
            <td class="text-center">{{ vampiro.disciplinaVampiros?.length || 0 }}</td>
            
            <td class="actions">
              <button class="btn-edit" @click="irEditar(vampiro.id)">Editar</button>
              <button class="btn-delete" @click="eliminar(vampiro.id)">Destruir</button>
            </td>
          </tr>
          <tr v-if="vampiros.length === 0">
            <td colspan="6" class="text-center">No hay vástagos en esta ciudad.</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { api } from '../../services/api';
import Swal from 'sweetalert2';
import Header from '../../components/Header.vue';

const router = useRouter();
const vampiros = ref([]);

const cargarVampiros = async () => {
  try {
    const data = await api.get('/api/Vampiros');
    vampiros.value = data;
  } catch (error) {
    console.error("Error al cargar los vampiros", error);
  }
};

const irCrear = () => {
  router.push({ name: 'VampiroCreate' }); 
};

const irEditar = (id) => {
  router.push({ path: '/vampiros/editar', query: { id: id } }); 
};

const eliminar = async (id) => {
  const result = await Swal.fire({
    title: '¿Muerte Definitiva?',
    text: "Esta acción destruirá al vástago para siempre.",
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#8a0303',
    cancelButtonColor: '#4a4a4a',
    confirmButtonText: 'Sí, destruir',
    cancelButtonText: 'Perdonar',
    background: '#1c1c1e',
    color: '#d4d4d4'
  });

  if (result.isConfirmed) {
    try {
      await api.remove(`/api/Vampiros/${id}`);
      Swal.fire({
        icon: 'success',
        title: 'Cenizas a las cenizas',
        text: 'El vampiro ha sido eliminado.',
        background: '#1c1c1e',
        color: '#d4d4d4',
        confirmButtonColor: '#8a0303'
      });
      await cargarVampiros();
    } catch (error) {
      console.error("Error al eliminar", error);
    }
  }
};

onMounted(() => {
  cargarVampiros();
});
</script>

<style scoped>
.vampire-container {
  background-color: #1c1c1e;
  color: #d4d4d4;
  padding: 2rem;
  border-radius: 8px;
  min-height: 80vh;
}

.header-actions {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 2rem;
  border-bottom: 1px solid #3a3a3c;
  padding-bottom: 1rem;
}

.table-responsive {
  width: 100%;
  overflow-x: auto;
  -webkit-overflow-scrolling: touch;
}

.vampire-table {
  width: 100%;
  border-collapse: collapse;
  min-width: 800px; /* Un poco más ancho para las nuevas columnas */
}

.vampire-table th, .vampire-table td {
  padding: 12px;
  text-align: left;
  border-bottom: 1px solid #3a3a3c;
}

.vampire-table th {
  color: #8a0303;
  text-transform: uppercase;
}

.btn-blood {
  background-color: #8a0303;
  color: #fff;
  border: none;
  padding: 10px 20px;
  cursor: pointer;
  border-radius: 4px;
  font-weight: bold;
}

.btn-blood:hover { background-color: #610202; }

.actions button {
  margin-right: 10px;
  padding: 6px 12px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.btn-edit { background-color: #4a4a4a; color: white; }
.btn-edit:hover { background-color: #6a6a6a; }

.btn-delete { background-color: #1c1c1e; color: #8a0303; border: 1px solid #8a0303 !important; }
.btn-delete:hover { background-color: #8a0303; color: white; }

.text-center { text-align: center !important; }

/* --- RESPONSIVE --- */
@media (max-width: 768px) {
  .header-actions { flex-direction: column; align-items: flex-start; gap: 1rem; }
  .header-actions button { width: 100%; }
  .actions { display: flex; flex-direction: column; gap: 0.5rem; }
  .actions button { margin-right: 0; width: 100%; }
}
</style>