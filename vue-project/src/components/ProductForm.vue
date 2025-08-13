<template>
  <form @submit.prevent="handleSubmit">
    <div class="mb-3">
      <label class="form-label">Nombre</label>
      <input v-model="form.name" type="text" class="form-control" required />
    </div>
    <div class="mb-3">
      <label class="form-label">Descripción</label>
      <input v-model="form.description" type="text" class="form-control" required />
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
    <button type="button" class="btn btn-secondary" @click="$emit('cancelar')">Cancelar</button>
  </form>
</template>

<script setup lang="ts">
import { ref, watch, defineProps, defineEmits } from "vue";
import { IProducto } from "../constantes";

const props = defineProps<{
  producto?: IProducto | null;
}>();

const emits = defineEmits(["guardar", "cancelar"]);

const isEdit = ref(false);

const form = ref({
  name: "",
  description: "",
  price: 0,
  stock: 0,
});

watch(
  () => props.producto,
  (nuevo) => {
    if (nuevo) {
      form.value = {
        name: nuevo.name,
        description: nuevo.description,
        price: nuevo.price,
        stock: nuevo.stock,
      };
      isEdit.value = true;
    } else {
      form.value = {
        name: "",
        description: "",
        price: 0,
        stock: 0,
      };
      isEdit.value = false;
    }
  },
  { immediate: true }
);

function handleSubmit() {
  emits("guardar", { ...form.value });
}
</script>