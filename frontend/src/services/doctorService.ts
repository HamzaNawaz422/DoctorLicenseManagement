import {
  ApiResponse,
  CreateDoctorRequest,
  Doctor,
  PagedResult,
  UpdateDoctorRequest,
} from "@/src/types/doctor";

const API_BASE_URL =
  process.env.NEXT_PUBLIC_API_BASE_URL ||
  "https://localhost:44345/api";

export async function getDoctors(
  search?: string,
  status?: string,
  pageNumber: number = 1,
  pageSize: number = 10
) {
  const params = new URLSearchParams();

  if (search) params.append("search", search);
  if (status) params.append("status", status);

  params.append("pageNumber", pageNumber.toString());
  params.append("pageSize", pageSize.toString());

  const response = await fetch(
    `${API_BASE_URL}/doctors?${params.toString()}`
  );

  if (!response.ok) {
    throw new Error("Failed to fetch doctors");
  }

  const result: ApiResponse<PagedResult<Doctor>> =
    await response.json();

  return result.data;
}

export async function createDoctor(data: CreateDoctorRequest) {
  const response = await fetch(`${API_BASE_URL}/doctors`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    throw new Error("Failed to create doctor");
  }

  const result: ApiResponse<Doctor> = await response.json();
  return result.data;
}

export async function updateDoctor(id: string, data: UpdateDoctorRequest) {
  const response = await fetch(`${API_BASE_URL}/doctors/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    throw new Error("Failed to update doctor");
  }

  return response.json();
}

export async function deleteDoctor(id: string) {
  const response = await fetch(`${API_BASE_URL}/doctors/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) {
    throw new Error("Failed to delete doctor");
  }

  return response.json();
}