<template>
  <div class="container mt-4">
    <h2>{{ isEdit ? "Editar Producto" : "Crear Producto" }}</h2>

    <div v-if="loading">Cargando...</div>
    <div v-if="error" class="alert alert-danger">{{ error }}</div>

    <form v-if="!loading" @submit.prevent="handleSubmit">
      <div class="mb-3">
        <label class="form-label">Nombre</label>
        <input v-model="form.name" type="text" class="form-control" required />
      </div>

      <div class="mb-3">
        <label class="form-label">Descripción</label>
        <textarea v-model="form.description" class="form-control" required></textarea>
      </div>

      <div class="mb-3">
        <label class="form-label">Precio</label>
        <input v-model.number="form.price" type="number" class="form-control" required min="0" />
      </div>

      <div class="mb-3">
        <label class="form-label">Stock</label>
        <input v-model.number="form.stock" type="number" class="form-control" required min="0" />
      </div>

      <button type="submit" class="btn btn-success me-2">
        {{ isEdit ? "Actualizar" : "Crear" }}
      </button>
      <button type="button" class="btn btn-secondary" @click="router.push('/productos')">Cancelar</button>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, defineProps } from "vue";
import { useRouter } from "vue-router";
import { API, IProducto } from "../constantes";

const props = defineProps<{ id?: string }>();
const router = useRouter();

const isEdit = ref(!!props.id);
const loading = ref(false);
const error = ref<string | null>(null);

const form = ref({
  name: "",
  description: "",
  price: 0,
  stock: 0,
});

// Si es editar, cargar producto
watch(
  () => props.id,
  async (id) => {
    if (id) {
      isEdit.value = true;
      loading.value = true;
      error.value = null;
      try {
        const res = await fetch(`${API}/Products/${id}`);
        if (!res.ok) throw new Error(`HTTP ${res.status}`);
        const data: IProducto = await res.json();
        form.value = { ...data };
      } catch (e: any) {
        error.value = e.message ?? String(e);
      } finally {
        loading.value = false;
      }
    }
  },
  { immediate: true }
);

async function handleSubmit() {
  loading.value = true;
  error.value = null;
  try {
    if (isEdit.value) {
      await fetch(`${API}/Products/${props.id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ ...form.value, id: Number(props.id) }),
      });
    } else {
      await fetch(`${API}/Products`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(form.value),
      });
    }
    router.push("/productos");
  } catch (e: any) {
    error.value = e.message ?? String(e);
  } finally {
    loading.value = false;
  }
}
</script>
