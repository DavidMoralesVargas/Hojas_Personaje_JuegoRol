import { createRouter, createWebHistory } from "vue-router";

//Importamos las paginas
import Inicio from "../Pages/Inicio.vue";
import Login from "../Pages/Auth/login.vue";

//Creamos las rutas
const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: "/",
            name: "Inicio",
            component: Inicio
        },
        {
            path: "/login",
            name: "Abrazar",
            component: Login
        }
    ]
})

export default router;