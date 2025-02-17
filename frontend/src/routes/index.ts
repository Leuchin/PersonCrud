import { createRouter, createWebHistory, type RouteRecordRaw } from "vue-router";
import List from "../views/list.vue";
import About from "../views/about.vue";
import Form from "../views/form.vue";

//o app.vue, não necessita de alterações pois ele já é o root
//o arquivo routes/index.ts é criado para que seja possível a navegação entre as páginas
const routes: Array<RouteRecordRaw> = [
  {path: "/", name: "Formulário", component: Form},
  {path: "/list", name: "List", component: List},
  {path: "/about", name: "About", component: About},
]

const router = createRouter({
    history: createWebHistory(),
    routes, 
})

export default router;