import { createRouter, createWebHistory } from "vue-router";

//Importamos las paginas
import Inicio from "../Pages/Inicio.vue";
import Hola from "../Pages/hola.vue";


//Creamos las rutas
const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: "/",
            name: "Inicio",
            component: Inicio
        }
    ]
})

export default router;