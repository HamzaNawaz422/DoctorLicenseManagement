import { Doctor } from "@/src/types/doctor";

type Props = {
  doctors: Doctor[];
  onDelete: (id: string) => void;
  onEdit: (doctor: Doctor) => void;
};

function StatusBadge({ status }: { status: Doctor["status"] }) {
  const styles = {
    Active: "bg-green-100 text-green-700",
    Expired: "bg-red-100 text-red-700",
    Suspended: "bg-yellow-100 text-yellow-700",
  };

  return (
    <span
      className={`rounded-full px-3 py-1 text-xs font-medium ${styles[status]}`}
    >
      {status}
    </span>
  );
}

export default function DoctorTable({ doctors, onDelete, onEdit }: Props) {
  if (doctors.length === 0) {
    return (
      <div className="rounded-2xl border border-dashed border-gray-300 bg-white p-10 text-center">
        <h3 className="text-lg font-semibold text-gray-800">
          No doctors found
        </h3>
        <p className="mt-2 text-sm text-gray-500">
          Try adjusting your search or add a new doctor.
        </p>
      </div>
    );
  }

  return (
    <div className="overflow-hidden rounded-2xl border border-slate-200 bg-white shadow-sm">
      <table className="w-full text-left text-sm">
        <thead className="bg-slate-50 text-slate-700">
          <tr>
            <th className="px-6 py-4">Doctor</th>
            <th className="px-6 py-4">Specialization</th>
            <th className="px-6 py-4">License No.</th>
            <th className="px-6 py-4">Expiry Date</th>
            <th className="px-6 py-4">Status</th>
            <th className="px-6 py-4 text-right">Actions</th>
          </tr>
        </thead>

        <tbody className="divide-y divide-gray-100">
          {doctors.map((doctor) => (
            <tr
              key={doctor.id}
              className={`transition ${
  doctor.status === "Expired"
    ? "bg-red-50 hover:bg-red-100"
    : "hover:bg-blue-50/40"
}`}
            >
              <td className="px-6 py-4">
                <div className="font-medium text-gray-900">
                  {doctor.fullName}
                </div>
                <div className="text-gray-500">{doctor.email}</div>
              </td>

              <td className="px-6 py-4 text-gray-700">
                {doctor.specialization}
              </td>

              <td className="px-6 py-4 text-gray-700">
                {doctor.licenseNumber}
              </td>

              <td className="px-6 py-4 text-gray-700">
                {new Date(doctor.licenseExpiryDate).toLocaleDateString()}
              </td>

              <td className="px-6 py-4">
                <StatusBadge status={doctor.status} />
              </td>

              <td className="px-6 py-4">
                <div className="flex justify-end gap-2">
                  <button
                    onClick={() => onEdit(doctor)}
                    className="rounded-lg bg-blue-50 px-3 py-1.5 text-xs font-medium text-blue-700 transition hover:bg-blue-100"
                  >
                    Edit
                  </button>

                  <button
                    onClick={() => onDelete(doctor.id)}
                    className="rounded-lg bg-red-50 px-3 py-1.5 text-xs font-medium text-red-700 transition hover:bg-red-100"
                  >
                    Delete
                  </button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
        
      </table>
      
    </div>
  );
}