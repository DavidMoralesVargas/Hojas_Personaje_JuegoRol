<template>
    <Header />
  <div class="vampire-container form-container">
    <h2>Abrazar Nuevo Vástago</h2>
    <hr class="divider" />

    <form @submit.prevent="guardarVampiro" class="vampire-form">
      <div class="form-group">
        <label>Nombre del Vampiro</label>
        <input v-model="vampiro.nombre" type="text" required placeholder="Ej: Lucita de Aragón" />
      </div>

      <div class="form-group-row">
        <div class="form-group">
          <label>Clan Bane (Debilidad)</label>
          <input v-model="vampiro.clanBane.bane" type="text" placeholder="Ej: Furia fácil" />
        </div>
        <div class="form-group">
          <label>Compulsión</label>
          <input v-model="vampiro.clanBane.compulsion" type="text" placeholder="Ej: Sed de sangre" />
        </div>
      </div>

      <div class="disciplinas-section">
        <h3>Disciplinas</h3>
        <div class="dual-list-box">
          
          <div class="list-box">
            <h4>Disponibles</h4>
            <div class="items-container">
              <div class="list-item" v-for="disc in disciplinasDisponibles" :key="disc.id">
                <span>{{ disc.nombre }}</span>
                <button type="button" class="btn-add" @click="agregarDisciplina(disc)">+</button>
              </div>
              <div v-if="disciplinasDisponibles.length === 0" class="empty-msg">No hay más disciplinas.</div>
            </div>
          </div>

          <div class="list-box selected-box">
            <h4>Adquiridas</h4>
            <div class="items-container">
              <div class="list-item" v-for="disc in vampiro.disciplinas" :key="disc.id">
                <span>{{ disc.nombre }}</span>
                <button type="button" class="btn-remove" @click="quitarDisciplina(disc)">-</button>
              </div>
              <div v-if="vampiro.disciplinas.length === 0" class="empty-msg">Ninguna disciplina seleccionada.</div>
            </div>
          </div>

        </div>
      </div>

      <div class="form-actions">
        <button type="button" class="btn-cancel" @click="cancelar">Cancelar</button>
        <button type="submit" class="btn-blood">Guardar Vástago</button>
      </div>
    </form>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { api } from '../../services/api';
import Swal from 'sweetalert2';
import Header from '../../components/Header.vue';

const router = useRouter();

const vampiro = ref({
  nombre: '',
  clanBane: {
    bane: '',
    compulsion: ''
  },
  disciplinas: []
});

const disciplinasDisponibles = ref([]);

const cargarDisciplinas = async () => {
  try {
    const data = await api.get('/api/Disciplinas/combo');
    disciplinasDisponibles.value = data;
  } catch (error) {
    console.error("Error al cargar disciplinas", error);
  }
};

const agregarDisciplina = (disciplina) => {
  vampiro.value.disciplinas.push(disciplina);
  disciplinasDisponibles.value = disciplinasDisponibles.value.filter(d => d.id !== disciplina.id);
};

const quitarDisciplina = (disciplina) => {
  disciplinasDisponibles.value.push(disciplina);
  vampiro.value.disciplinas = vampiro.value.disciplinas.filter(d => d.id !== disciplina.id);
};

const guardarVampiro = async () => {
  // VALIDACIÓN: Un vampiro no puede estar sin disciplinas
  if (vampiro.value.disciplinas.length === 0) {
    Swal.fire({
      icon: 'warning',
      title: 'Vástago Indefenso',
      text: 'Por orden del Príncipe, todo vampiro debe tener al menos una Disciplina adquirida.',
      background: '#1c1c1e',
      color: '#d4d4d4',
      confirmButtonColor: '#8a0303'
    });
    return; // Detenemos la ejecución aquí
  }

  try {
    await api.post('/api/Vampiros', vampiro.value);
    Swal.fire({
      icon: 'success',
      title: 'Vástago Creado',
      background: '#1c1c1e',
      color: '#d4d4d4',
      confirmButtonColor: '#8a0303'
    });
    router.push({ path: '/vampiros' });
  } catch (error) {
    console.error(error);
  }
};

const cancelar = () => {
  router.push({ path: '/vampiros' });
};

onMounted(() => {
  cargarDisciplinas();
});
</script>

<style scoped>
.vampire-container { background-color: #1c1c1e; color: #d4d4d4; padding: 2rem; border-radius: 8px; }
.divider { border-color: #3a3a3c; margin-bottom: 2rem; }
.form-group { margin-bottom: 1.5rem; display: flex; flex-direction: column; }
.form-group-row { display: grid; grid-template-columns: 1fr 1fr; gap: 1.5rem; }
label { color: #8a0303; font-weight: bold; margin-bottom: 0.5rem; }
input { background-color: #2d2d30; border: 1px solid #4a4a4a; color: #fff; padding: 10px; border-radius: 4px; }
input:focus { outline: none; border-color: #8a0303; }
.dual-list-box { display: grid; grid-template-columns: 1fr 1fr; gap: 2rem; margin-top: 1rem; }
.list-box { background-color: #252526; border: 1px solid #3a3a3c; border-radius: 4px; padding: 1rem; }
.list-box h4 { margin-top: 0; color: #a9a9a9; border-bottom: 1px solid #3a3a3c; padding-bottom: 5px; }
.items-container { min-height: 150px; max-height: 300px; overflow-y: auto; }
.list-item { display: flex; justify-content: space-between; align-items: center; padding: 8px; background-color: #2d2d30; margin-bottom: 5px; border-radius: 4px; }
.btn-add { background-color: #28a745; color: white; border: none; border-radius: 50%; width: 25px; height: 25px; cursor: pointer; }
.btn-remove { background-color: #dc3545; color: white; border: none; border-radius: 50%; width: 25px; height: 25px; cursor: pointer; }
.empty-msg { font-style: italic; color: #777; text-align: center; margin-top: 20px; }
.form-actions { display: flex; justify-content: flex-end; gap: 1rem; margin-top: 2rem; }
.btn-blood { background-color: #8a0303; color: #fff; border: none; padding: 10px 20px; cursor: pointer; border-radius: 4px; font-weight: bold; }
.btn-cancel { background-color: transparent; color: #d4d4d4; border: 1px solid #4a4a4a; padding: 10px 20px; cursor: pointer; border-radius: 4px; }
.btn-cancel:hover { background-color: #4a4a4a; }
@media (max-width: 768px) {
  .form-group-row { grid-template-columns: 1fr; gap: 0; }
  .dual-list-box { grid-template-columns: 1fr; gap: 1rem; }
  .form-actions { flex-direction: column-reverse; gap: 1rem; }
  .btn-blood, .btn-cancel { width: 100%; text-align: center; }
}
</style>