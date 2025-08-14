<template>
  <div>
    <router-link to="/productos/crear" class="btn btn-primary mb-3">Crear producto</router-link>
    <div v-if="loading">Cargando...</div>
    <div v-if="error">{{ error }}</div>

    <table class="table" v-if="!loading && !error">
      <thead>
        <tr>
          <th scope="col">id</th>
          <th scope="col">Nombre</th>
          <th scope="col">Descripción</th>
          <th scope="col">Precio</th>
          <th scope="col">Stock</th>
          <th scope="col">Acciones</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="producto in productos" :key="producto.id">
          <th scope="row">{{ producto.id }}</th>
          <td>{{ producto.name }}</td>
          <td>{{ producto.description }}</td>
          <td>{{ producto.price }}</td>
          <td>{{ producto.stock }}</td>
          <td>
            <button class="btn btn-danger btn-sm me-2" @click="eliminar(producto.id)">Eliminar</button>
            <router-link :to="`/productos/editar/${producto.id}`" class="btn btn-warning btn-sm">Editar</router-link>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router"; 
import { API, IProducto } from "../constantes";

const productos = ref<IProducto[]>([]);
const loading = ref(false);
const error = ref<string | null>(null);

async function cargarProductos() {
  loading.value = true;
  try {
    const res = await fetch(`${API}/Products`);
    if (!res.ok) throw new Error(`HTTP ${res.status}`);
    const data = (await res.json()) as IProducto[];
    productos.value = data;
  } catch (e: any) {
    error.value = e.message ?? String(e);
  } finally {
    loading.value = false;
  }
}

async function eliminar(id: number) {
  if (confirm("¿Seguro que deseas eliminar este producto?")) {
    await fetch(`${API}/Products/${id}`, { method: "DELETE" });
    cargarProductos();
  }
}

onMounted(cargarProductos);
</script> 