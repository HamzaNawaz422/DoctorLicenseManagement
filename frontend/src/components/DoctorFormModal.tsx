"use client";

import { useEffect, useState } from "react";
import { CreateDoctorRequest, Doctor } from "@/src/types/doctor";

type Props = {
  isOpen: boolean;
  selectedDoctor: Doctor | null;
  onClose: () => void;
  onSubmit: (data: CreateDoctorRequest) => Promise<void>;
};

export default function DoctorFormModal({
  isOpen,
  selectedDoctor,
  onClose,
  onSubmit,
}: Props) {
  const [form, setForm] = useState<CreateDoctorRequest>({
    fullName: "",
    email: "",
    specialization: "",
    licenseNumber: "",
    licenseExpiryDate: "",
  });

  const [saving, setSaving] = useState(false);

  useEffect(() => {
    if (selectedDoctor) {
      setForm({
        fullName: selectedDoctor.fullName,
        email: selectedDoctor.email,
        specialization: selectedDoctor.specialization,
        licenseNumber: selectedDoctor.licenseNumber,
        licenseExpiryDate: selectedDoctor.licenseExpiryDate.split("T")[0],
      });
    } else {
      setForm({
        fullName: "",
        email: "",
        specialization: "",
        licenseNumber: "",
        licenseExpiryDate: "",
      });
    }
  }, [selectedDoctor, isOpen]);

  if (!isOpen) return null;

  function updateField(field: keyof CreateDoctorRequest, value: string) {
    setForm((prev) => ({
      ...prev,
      [field]: value,
    }));
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    try {
      setSaving(true);
      await onSubmit(form);
      onClose();
    } finally {
      setSaving(false);
    }
  }

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40 p-4">
      <div className="w-full max-w-xl rounded-3xl border border-slate-200 bg-white p-8 shadow-2xl">
        <div className="mb-6 flex items-center justify-between">
          <div>
            <h2 className="text-xl font-semibold text-gray-900">
              {selectedDoctor ? "Edit Doctor" : "Add Doctor"}
            </h2>
            <p className="text-sm text-gray-500">
              {selectedDoctor
                ? "Update doctor license details"
                : "Enter doctor license details"}
            </p>
          </div>

          <button
            type="button"
            onClick={onClose}
            className="rounded-lg px-3 py-1 text-gray-500 hover:bg-gray-100"
          >
            ✕
          </button>
        </div>

        <form onSubmit={handleSubmit} className="space-y-4">
          <input
            required
            type="text"
            placeholder="Full Name"
            value={form.fullName}
            onChange={(e) => updateField("fullName", e.target.value)}
            className="w-full rounded-lg border border-gray-300 px-4 py-2 outline-none focus:border-blue-500"
          />

          <input
            required
            type="email"
            placeholder="Email"
            value={form.email}
            onChange={(e) => updateField("email", e.target.value)}
            className="w-full rounded-lg border border-gray-300 px-4 py-2 outline-none focus:border-blue-500"
          />

          <input
            required
            type="text"
            placeholder="Specialization"
            value={form.specialization}
            onChange={(e) => updateField("specialization", e.target.value)}
            className="w-full rounded-lg border border-gray-300 px-4 py-2 outline-none focus:border-blue-500"
          />

          <input
            required
            type="text"
            placeholder="License Number"
            value={form.licenseNumber}
            onChange={(e) => updateField("licenseNumber", e.target.value)}
            className="w-full rounded-lg border border-gray-300 px-4 py-2 outline-none focus:border-blue-500"
          />

          <input
            required
            type="date"
            value={form.licenseExpiryDate}
            onChange={(e) => updateField("licenseExpiryDate", e.target.value)}
            className="w-full rounded-lg border border-gray-300 px-4 py-2 outline-none focus:border-blue-500"
          />

          <div className="flex justify-end gap-3 pt-4">
            <button
              type="button"
              onClick={onClose}
              className="rounded-lg border border-gray-300 px-4 py-2 text-gray-700 hover:bg-gray-50"
            >
              Cancel
            </button>

            <button
              type="submit"
              disabled={saving}
              className="rounded-xl bg-gradient-to-r from-blue-600 to-indigo-600 px-5 py-2.5 text-white shadow-md transition hover:shadow-lg disabled:opacity-60"
            >
              {saving
                ? "Saving..."
                : selectedDoctor
                ? "Update Doctor"
                : "Save Doctor"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}