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

const isClient = typeof window !== 'undefined';

const baseURL = isClient && window.location.hostname === 'localhost'
  ? "http://localhost:5000/api"  
  : process.env.NODE_ENV === 'production' 
    ? "http://api:5000/api"     
    : "http://localhost:5000/api";

export const api = axios.create({
  baseURL
});
