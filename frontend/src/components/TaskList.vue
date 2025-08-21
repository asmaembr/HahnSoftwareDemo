<script setup lang="ts">
import { ref, onMounted } from "vue";
import { api } from "../services/api";

const tasks = ref<any[]>([]);
const newTask = ref("");

async function load() {
  tasks.value = (await api.get("/Tasks")).data;
}

async function addTask() {
  if (!newTask.value) return;
  await api.post("/Tasks", { title: newTask.value });
  newTask.value = "";
  await load();
}

onMounted(load);
</script>

<template>
  <div class="task-container">
    <h1>✅ Tasks</h1>

    <!-- Add Task -->
    <div class="task-form">
      <input v-model="newTask" placeholder="New task" />
      <button @click="addTask">Add</button>
    </div>

    <!-- Task List -->
    <ul class="task-list">
      <li v-for="t in tasks" :key="t.id" class="task-item">
        <span :class="{ completed: t.isCompleted }">
          {{ t.title }}
        </span>
        <span class="status">{{ t.isCompleted ? "✔" : "❌" }}</span>
      </li>
    </ul>
  </div>
</template>
