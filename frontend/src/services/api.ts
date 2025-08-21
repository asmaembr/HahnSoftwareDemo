/*
 * Copyright (c) 2025 Asmae Moubarriz
 * All rights reserved.
 * 
 * This is a technical assignment submission to Hahn Software.
 * 
 * Author: Asmae Moubarriz
 * Created: August 2025
 */

import axios from "axios";

// Detect if we're running in a browser (client-side) vs server-side
const isClient = typeof window !== 'undefined';

const baseURL = isClient && window.location.hostname === 'localhost'
  ? "http://localhost:5000/api"  // Browser accessing via localhost
  : process.env.NODE_ENV === 'production' 
    ? "http://api:5000/api"      // Container-to-container communication
    : "http://localhost:5000/api"; // Development mode

export const api = axios.create({
  baseURL
});
