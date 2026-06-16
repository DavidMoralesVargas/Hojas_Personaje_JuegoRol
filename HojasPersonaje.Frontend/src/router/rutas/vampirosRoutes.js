import VampiroIndex from "../../Pages/Vampiros/vampiroIndex.vue";
import VampiroCreate from "../../Pages/Vampiros/vampiroCreate.vue";
import VampiroEdit from "../../Pages/Vampiros/vampiroEdit.vue";

export default[
    {
        path: "/vampiros",
        name: "VampirosIndex",
        component: VampiroIndex,
        meta: { requiereAuth: true },
    },
    {
        path: "/vampiros/crear",
        name: "VampiroCreate",
        component: VampiroCreate,
        meta: { requiereAuth: true },
    },
    {
        path: "/vampiros/:id",
        name: "VampiroEdit",
        component: VampiroEdit,
        meta: { requiereAuth: true },
    }
]
