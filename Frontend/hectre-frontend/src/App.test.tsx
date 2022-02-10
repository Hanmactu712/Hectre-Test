import { render, screen } from "@testing-library/react";
import React from "react";
import App from "./App";

test("renders learn react link", () => {
  render(<App />);
  const chemicalElements = screen.getAllByText(/CHEMICALS/i);
  expect(chemicalElements.length).toBeGreaterThan(0);

  const titleChemical = screen.getByText("Chemicals");
  expect(titleChemical).toBeInTheDocument();
});
