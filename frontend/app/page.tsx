"use client";

import { useEffect, useState } from "react";
import DoctorTable from "@/src/components/DoctorTable";
import DoctorFormModal from "@/src/components/DoctorFormModal";
import {
  createDoctor,
  deleteDoctor,
  getDoctors,
  updateDoctor,
} from "@/src/services/doctorService";
import { CreateDoctorRequest, Doctor } from "@/src/types/doctor";

export default function Home() {
  const [doctors, setDoctors] = useState<Doctor[]>([]);
  const [loading, setLoading] = useState(true);
  const [search, setSearch] = useState("");
  const [status, setStatus] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedDoctor, setSelectedDoctor] = useState<Doctor | null>(null);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(5);
  const [totalPages, setTotalPages] = useState(1);
  async function loadDoctors() {
    try {
      setLoading(true);
       const data = await getDoctors(
  search,
  status,
  pageNumber,
  pageSize
);

    setDoctors(data.items ?? []);
    setTotalPages(data.totalPages);
    } catch (error) {
      console.error(error);
      alert("Failed to load doctors");
    } finally {
      setLoading(false);
    }
  }

  async function handleSaveDoctor(data: CreateDoctorRequest) {
  try {
    if (selectedDoctor) {
      await updateDoctor(selectedDoctor.id, data);
    } else {
      await createDoctor(data);
    }

    setSelectedDoctor(null);
    setIsModalOpen(false);
    await loadDoctors();
  } catch (error) {
    console.error(error);
    alert("Failed to save doctor");
  }
}

  async function handleDeleteDoctor(id: string) {
    const confirmed = confirm("Are you sure you want to delete this doctor?");

    if (!confirmed) return;

    try {
      await deleteDoctor(id);
      await loadDoctors();
    } catch (error) {
      console.error(error);
      alert("Failed to delete doctor");
    }
  }

  function handleEditDoctor(doctor: Doctor) {
    setSelectedDoctor(doctor);
    setIsModalOpen(true);
  }

  function handleAddDoctor() {
    setSelectedDoctor(null);
    setIsModalOpen(true);
  }

  function handleSearch() {
    setPageNumber(1);
    loadDoctors();
  }

  function handleClear() {
  setSearch("");
  setStatus("");
  setPageNumber(1);

  setTimeout(() => {
    loadDoctors();
  }, 0);
}

  useEffect(() => {
  loadDoctors();
}, [pageNumber]);

  return (
    <main className="min-h-screen bg-gradient-to-br from-slate-50 to-blue-50 p-8">
      <div className="mx-auto max-w-7xl">
        <div className="mb-8 flex items-center justify-between rounded-2xl bg-white p-6 shadow-sm border border-slate-200">
          <div>
            <h1 className="text-3xl font-bold text-slate-800">
              Doctor License Management
            </h1>
            <p className="mt-2 text-gray-600">Medical SaaS Platform</p>
          </div>

          <button
            onClick={handleAddDoctor}
            className="rounded-xl bg-gradient-to-r from-blue-600 to-indigo-600 px-5 py-2.5 text-white shadow-md transition hover:scale-[1.02] hover:shadow-lg"
          >
            Add Doctor
          </button>
        </div>

        <div className="mb-6 rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
          <div className="grid gap-4 md:grid-cols-4">
            <input
              type="text"
              placeholder="Search by name or license..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              className="rounded-lg border border-gray-300 px-4 py-2 text-sm text-slate-900 placeholder:text-slate-400 outline-none focus:border-blue-500"
            />

            <select
              value={status}
              onChange={(e) => setStatus(e.target.value)}
              className="rounded-lg border border-gray-300 px-4 py-2 text-sm text-slate-900 outline-none focus:border-blue-500"
            >
              <option value="">All Status</option>
              <option value="1">Active</option>
              <option value="2">Expired</option>
              <option value="3">Suspended</option>
            </select>

            <button
              onClick={handleSearch}
              className="rounded-xl bg-gradient-to-r from-slate-800 to-slate-700 px-4 py-2 text-sm text-white transition hover:shadow-md"
            >
              Search
            </button>

            <button
              onClick={handleClear}
              className="rounded-lg border border-gray-300 px-4 py-2 text-sm text-gray-700 hover:bg-gray-50"
            >
              Clear
            </button>
          </div>
        </div>

        {loading ? (
          <div className="rounded-xl bg-white p-8 text-center text-gray-500 shadow-sm">
            Loading doctors...
          </div>
        ) : (
          <>
  <DoctorTable
    doctors={doctors}
    onDelete={handleDeleteDoctor}
    onEdit={handleEditDoctor}
  />

  <div className="mt-6 flex items-center justify-center gap-3">
    <button
      disabled={pageNumber === 1}
      onClick={() => setPageNumber((prev) => prev - 1)}
      className="rounded-lg border border-gray-300 px-4 py-2 text-sm text-gray-700 hover:bg-gray-50 disabled:opacity-50"
      
    >
      Previous
    </button>

    <span className="text-sm text-slate-600">
      Page {pageNumber} of {totalPages}
    </span>

    <button
      disabled={pageNumber === totalPages}
      onClick={() => setPageNumber((prev) => prev + 1)}
      className="rounded-lg border border-gray-300 px-4 py-2 text-sm text-gray-700 hover:bg-gray-50 disabled:opacity-50"
    >
      Next
    </button>
  </div>
</>
          
        )}
      </div>

      <DoctorFormModal
        isOpen={isModalOpen}
        selectedDoctor={selectedDoctor}
        onClose={() => {
  setIsModalOpen(false);
  setSelectedDoctor(null);
}}
        onSubmit={handleSaveDoctor}
      />
    </main>
  );
}