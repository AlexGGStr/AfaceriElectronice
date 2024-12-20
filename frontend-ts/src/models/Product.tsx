import Image from "./Image";

interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  categoryId: number;
  discount: any;
  quantity: number;
  images: Image[];
}

export default Product;
