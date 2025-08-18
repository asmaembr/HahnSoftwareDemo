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

<style scoped>
/* Container */
.task-container {
  max-width: 600px;
  margin: 40px auto;
  padding: 20px;
  background: #ffffff;
  border-radius: 16px;
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s ease;
}

.task-container:hover {
  transform: translateY(-3px);
}

/* Title */
h1 {
  text-align: center;
  color: #2563eb;
  margin-bottom: 20px;
  font-size: 2rem;
}

/* Form */
.task-form {
  display: flex;
  margin-bottom: 20px;
}

.task-form input {
  flex: 1;
  padding: 10px 15px;
  border-radius: 8px 0 0 8px;
  border: 1px solid #ddd;
  outline: none;
  font-size: 16px;
  transition: border-color 0.2s ease;
}

.task-form input:focus {
  border-color: #2563eb;
}

.task-form button {
  padding: 10px 20px;
  border-radius: 0 8px 8px 0;
  border: none;
  background-color: #2563eb;
  color: white;
  font-weight: bold;
  cursor: pointer;
  transition: background 0.2s ease;
}

.task-form button:hover {
  background-color: #1d4ed8;
}

/* Task List */
.task-list {
  list-style: none;
  padding: 0;
  margin: 0;
}

.task-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 12px 16px;
  margin-bottom: 10px;
  border-radius: 12px;
  background: #f3f4f6;
  transition: box-shadow 0.2s ease;
}

.task-item:hover {
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
}

.completed {
  text-decoration: line-through;
  color: #888888;
}

.status {
  font-weight: bold;
  font-size: 1.1rem;
}
</style>
