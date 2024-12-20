import { useEffect, useState } from "react";
import Response from "../models/Response";

interface FetchOptions extends RequestInit {
  body?: BodyInit | null;
}

interface UseApiCallResult<T> {
  data: Response<T> | null;
  isLoading: boolean;
}

const useApiCall = <T,>(
  url: string,
  options: FetchOptions = {}
): UseApiCallResult<T> => {
  const [data, setData] = useState<Response<T> | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);

  const fetchData = async () => {
    try {
      const response = await fetch(url, options);
      const jsonData: Response<T> = await response.json();
      setData(jsonData);
      setIsLoading(false);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    fetchData();
  }, [url, JSON.stringify(options)]);

  return { data, isLoading };
};

export default useApiCall;
