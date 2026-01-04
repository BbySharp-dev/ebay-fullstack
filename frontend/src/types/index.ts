export interface Product {
  id: number;
  productName: string;
  description?: string;
  price: number;
  quantity: number;
  categoryId?: number;
  createDate: string;
  imageUrl?: string;
  ratingScore?: number;
  ratingCount: number;
  images: ProductImage[];
}

export interface ProductImage {
  id: number;
  productId: number;
  imageUrl: string;
  isPrimary: boolean;
  uploadDate: string;
}

export interface ApiError {
  message: string;
  status: number;
  details?: string;
}
