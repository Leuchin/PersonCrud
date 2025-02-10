import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';

export default defineConfig({
  plugins: [vue()],
  server: {
    proxy: {
      '/api': {
        target: 'http://localhost:5000', // Substitua pela URL do seu backend
        changeOrigin: true,
        secure: false,
      },
    },
  },
});

