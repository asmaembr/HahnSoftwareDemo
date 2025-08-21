<!--
 * Copyright (c) 2025 Asmae Moubarriz
 * All rights reserved.
 * 
 * This is a technical assignment submission to Hahn Software.
 * 
 * Author: Asmae Moubarriz
 * Created: August 2025
 -->

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

async function toggleTask(task: any) {
  try {
    await api.put(`/Tasks/${task.id}`, {
      id: task.id,
      title: task.title,
      isCompleted: !task.isCompleted,
    });
    await load();
  } catch (error) {
    console.error("Error toggling task:", error);
  }
}

async function deleteTask(taskId: string) {
  try {
    await api.delete(`/Tasks/${taskId}`, {
      data: { id: taskId },
    });
    await load();
  } catch (error) {
    console.error("Error deleting task:", error);
  }
}

onMounted(load);
</script>

<template>
  <div class="task-container">
    <h1>
      <img src="../../public/favicon.ico" width="50" height="50" />
      Task Manager
    </h1>

    <!-- Add Task -->
    <div class="task-form">
      <input v-model="newTask" placeholder="New task" />
      <button @click="addTask">Add</button>
    </div>

    <!-- Task Table -->
    <table class="task-table">
      <thead>
        <tr>
          <th>Title</th>
          <th>Status</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="t in tasks" :key="t.id">
          <td :class="{ completed: t.isCompleted }">{{ t.title }}</td>
          <td>
            <span :class="['status', t.isCompleted ? 'done' : 'pending']">
              {{ t.isCompleted ? "✔ Completed" : "❌ Pending" }}
            </span>
          </td>
          <td class="actions">
            <button @click="toggleTask(t)">Toggle</button>
            <button @click="deleteTask(t.id)">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
<!-- Interview Note -->
<div class="interview-note">  
  I hope this Demo app lives up to your expectations !!
</div>

</template>
<style>
html, body {
  margin: 0;
  padding: 0;
  height: 100%;
  background: linear-gradient(135deg, #0f2027, #203a43, #2c5364);
  font-family: "Segoe UI", sans-serif;
  color: white;
}

.task-container {
  height: 100%;
  display: flex;
  flex-direction: column;
  padding: 30px;
  backdrop-filter: blur(12px);
  background: transparent;
  margin: 0 auto;
  width: 800px;
}

/* Header */
h1 {
  text-align: center;
  color: #00d4ff;
  margin-bottom: 20px;
  font-size: 2.2rem;
  text-shadow: 0 0 10px rgba(0, 212, 255, 0.7);
}

.task-form {
  display: flex;
  margin-bottom: 25px;
  justify-content: center;
}

.task-form input {
  flex: 1;
  padding: 12px 15px;
  border-radius: 10px 0 0 10px;
  border: none;
  outline: none;
  font-size: 16px;
  background: rgba(255, 255, 255, 0.15);
  color: #fff;
  backdrop-filter: blur(8px);
}

.task-form input::placeholder {
  color: #ccc;
}

.task-form button {
  padding: 12px 20px;
  border-radius: 0 10px 10px 0;
  border: none;
  background: linear-gradient(135deg, #00d4ff, #2c5364);
  color: white;
  font-weight: bold;
  cursor: pointer;
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}

.task-form button:hover {
  transform: scale(1.05);
  box-shadow: 0 0 12px rgb(44, 83, 100, 0.8);
}

.task-table {
  width: 100%;
  border-collapse: collapse;
  background: rgba(255, 255, 255, 0.1);
  backdrop-filter: blur(10px);
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 8px 25px rgba(0, 0, 0, 0.3);
}

.task-table th,
.task-table td {
  padding: 15px;
  text-align: left;
}

.task-table th {
  background: rgba(0, 212, 255, 0.2);
  color: #00d4ff;
  text-transform: uppercase;
  font-weight: bold;
  letter-spacing: 1px;
}

.task-table tr {
  transition: background 0.2s ease;
}

.task-table tr:hover {
  background: rgba(255, 255, 255, 0.05);
}

.status {
  display: inline-block;
  padding: 6px 10px;
  border-radius: 999px;
  font-weight: 700;
  font-size: 0.95rem;
  line-height: 1;
  letter-spacing: 0.02em;
}

.status.done {
  background: rgba(168, 255, 120, 0.18);
  border: 1px solid rgba(168, 255, 120, 0.45);
  color: #a8ff78;
}

.status.pending {
  background: rgba(255, 107, 107, 0.16);
  border: 1px solid rgba(255, 107, 107, 0.45);
  color: #ff8a8a;
}

.completed {
  text-decoration: line-through;
  color: #bbb;
}

.actions {
  display: flex;
  gap: 10px;
}

.actions button {
  padding: 8px 14px;
  border: none;
  border-radius: 10px;
  background: linear-gradient(135deg, #00d4ff, #2c5364);
  color: white;
  font-weight: 700;
  cursor: pointer;
  transition: transform 0.18s ease, box-shadow 0.18s ease, opacity 0.18s ease;
  opacity: 0.95;
}

.actions button:hover {
  transform: translateY(-1px) scale(1.03);
  box-shadow: 0 0 12px rgb(44, 83, 100, 0.8);
  opacity: 1;
}

.interview-note {
  margin: 30px auto 0 auto;
  padding: 18px 24px;
  border-radius: 14px;
  background: rgba(255, 255, 255, 0.1);
  backdrop-filter: blur(10px);
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.25);
  border: 1px solid rgba(0, 212, 255, 0.35);
  color: #d1faff;
  font-size: 1rem;
  line-height: 1.6;
  font-style: italic;
  text-align: center;
  width: 100%;
  max-width: 800px;
  animation: fadeIn 0.6s ease-out forwards;
}

@keyframes fadeIn {
  0% {
    opacity: 0;
    transform: translateY(12px);
  }
  100% {
    opacity: 1;
    transform: translateY(0);
  }
}
</style>
