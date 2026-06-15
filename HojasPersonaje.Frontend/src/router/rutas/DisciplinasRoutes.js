import DisciplinasIndex from "../../Pages/Disciplinas/disciplinasIndex.vue";
import DisciplinaDetails from "../../Pages/Disciplinas/disciplinaDetails.vue";


export default[
    {
        path: "/disciplinas",
        name: "DisciplinasIndex",
        component: DisciplinasIndex,
        meta: { requiereAuth: true },
    },
    {
        path: "/disciplinas/:id",
        name: "DisciplinaDetails",
        component: DisciplinaDetails,
        meta: { requiereAuth: true },
    }
]
