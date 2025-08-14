import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Table from "../components/Table.vue";
import ProductForm from "../components/CustomerFrom.vue";

const routes: RouteRecordRaw[] = [
  { path: "/productos", component: Table },
  { path: "/productos/crear", component: ProductForm },
  { path: "/productos/editar/:id", component: ProductForm, props: true },
  { path: "/", redirect: "/productos" },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
