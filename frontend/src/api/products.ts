import { api } from './client';
import type { Product, ProductImage } from '../types';

export const productApi = {
  getAll: () => api.get<Product[]>('/api/products'),

  getById: (id: number) => api.get<Product>(`/api/products/${id}`),

  create: (product: Partial<Product>) =>
    api.post<Product, Partial<Product>>('/api/products', product),

  update: (id: number, product: Partial<Product>) =>
    api.put<Product, Partial<Product>>(`/api/products/${id}`, product),

  delete: (id: number) => api.delete<void>(`/api/products/${id}`),

  uploadImage: (id: number, file: File) =>
    api.uploadFile<ProductImage>(`/api/products/${id}/images`, file),

  deleteImage: (productId: number, imageId: number) =>
    api.delete<void>(`/api/products/${productId}/images/${imageId}`),
} as const;
