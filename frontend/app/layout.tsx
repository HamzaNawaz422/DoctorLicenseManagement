import type { Metadata } from "next";
import "./globals.css";

export const metadata: Metadata = {
  title: "Doctor License Management",
  description: "Medical SaaS Doctor License Management Module",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>{children}</body>
    </html>
  );
}