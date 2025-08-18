<script setup lang="ts">
import { ref, onMounted } from "vue";
import { api } from "../services/api";

const tasks = ref<any[]>([]);
const newTask = ref("");

async function load() {
  tasks.value = (await api.get("/tasks")).data;
}

async function addTask() {
  if (!newTask.value) return;
  await api.post("/tasks", { title: newTask.value });
  newTask.value = "";
  await load();
}

onMounted(load);
</script>

<template>
  <div>
    <h1>Tasks</h1>
    <input v-model="newTask" placeholder="New task" />
    <button @click="addTask">Add</button>

    <ul>
      <li v-for="t in tasks" :key="t.id">
        {{ t.title }} - {{ t.isCompleted ? "✔" : "❌" }}
      </li>
    </ul>
  </div>
</template>
