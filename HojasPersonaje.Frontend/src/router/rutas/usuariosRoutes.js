import UsuariosIndex from "../../Pages/Usuarios/UsuariosIndex.vue";


export default[
    {
        path: "/usuarios",
        name: "UsuariosIndex",
        component: UsuariosIndex,
        meta: { requiereAuth: true },
    }
]
