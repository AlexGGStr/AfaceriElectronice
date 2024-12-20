import CartItem from "../models/CartItem";
import backendUrl from "../config";
import Response from "../models/Response";

export async function addCartToDb(items: CartItem[]) {
  const cart = items;
  if (cart.length > 0) {
    const cartData = cart.map((item: CartItem) => {
      return { productId: item.product.id, quantity: item.quantity };
    });

    await fetch(`${backendUrl}/Cart`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
      body: JSON.stringify(cartData),
    });
  }
  const data = await fetch(`${backendUrl}/Cart`, {
    headers: {
      Authorization: `Bearer ${localStorage.getItem("token")}`,
      "Content-Type": "application/json",
    },
  });
  const response: Response<CartItem[]> = await data.json();
  if (response.success) {
    return response.data;
  }
}
