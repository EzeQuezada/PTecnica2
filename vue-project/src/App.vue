<template>
  <div class="container mt-4">
    <NavBar/>
    <Table
      v-if="!mostrandoFormulario"
      ref="tablaRef"
      @crear="mostrarFormularioCrear"
      @editar="mostrarFormularioEditar"
      @eliminar="eliminarProducto"
    />
    <ProductForm
      v-else
      :producto="productoEditando"
      @guardar="guardarProducto"
      @cancelar="cancelarFormulario"
    />
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import NavBar from "./components/NavBar.vue";
import Table from "./components/Table.vue";
import ProductForm from "./components/ProductForm.vue";
import { API, IProducto } from "./constantes";

const mostrandoFormulario = ref(false);
const productoEditando = ref<IProducto | null>(null);
const tablaRef = ref();

function mostrarFormularioCrear() {
  productoEditando.value = null;
  mostrandoFormulario.value = true;
}

function mostrarFormularioEditar(producto: IProducto) {
  productoEditando.value = producto;
  mostrandoFormulario.value = true;
}

function cancelarFormulario() {
  mostrandoFormulario.value = false;
}

async function guardarProducto(producto: Omit<IProducto, "id">) {
  if (productoEditando.value) {
    // Editar (PUT)
    await fetch(`${API}/Products/${productoEditando.value.id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ ...producto, id: productoEditando.value.id }),
    });
  } else {
    // Crear (POST)
    await fetch(`${API}/Products`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(producto),
    });
  }
  mostrandoFormulario.value = false;
  tablaRef.value.cargarProductos();
}

async function eliminarProducto(id: number) {
  if (confirm("¿Seguro que deseas eliminar este producto?")) {
    await fetch(`${API}/Products/${id}`, { method: "DELETE" });
    tablaRef.value.cargarProductos();
  }
}
</script>

<style scoped>
</style>