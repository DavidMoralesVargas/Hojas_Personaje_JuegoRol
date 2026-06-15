import { createRouter, createWebHistory } from "vue-router";

//Importamos las paginas
import Inicio from "../Pages/Inicio.vue";
import AuhtRoutes from "./rutas/AuhtRoutes.js";
import DisciplinasRoutes from "./rutas/DisciplinasRoutes.js";



//Creamos las rutas
const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: "/",
            name: "Inicio",
            component: Inicio
        },
        ...AuhtRoutes,
        ...DisciplinasRoutes
    ]
})


// Guard global - Se asegura de que las rutas que requieren autenticación no sean accesibles sin un token válido
router.beforeEach((to, from, next) => {
    const token = localStorage.getItem("Token");

    if (to.meta.requiereAuth && !token) {
        next("/");
    } else {
        next();
    }
});

export default router;