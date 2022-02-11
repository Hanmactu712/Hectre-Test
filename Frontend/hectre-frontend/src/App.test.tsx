import { cleanup, fireEvent, render, screen } from "@testing-library/react";
import React from "react";
import App from "./App";

afterEach(cleanup);

const mockChemicals = [
  {
    id: "6181eb674e68df4b5845299c",
    chemicalType: "Plant Growth Regulator",
    preHarvestIntervalInDays: "Up to 90% petal fall",
    activeIngredient: "SPINETORAM",
    name: "SERENADE OPTIMUM",
    creationDate: "2014-06-28T06:27:56-12:00",
    modificationDate: null,
    deletionDate: null,
  },
  {
    id: "6181eb67ce0fa0404dbf9752",
    chemicalType: "Surfactant",
    preHarvestIntervalInDays: 0,
    activeIngredient: "AMINOETHOXYVINYLGLYCINE (AVG)",
    name: "DIPEL DF",
    creationDate: "2014-10-19T12:41:11-13:00",
    modificationDate: null,
    deletionDate: "2019-05-30T03:11:26-12:00",
  },
  {
    id: "6181eb678556f8c194c22d8b",
    chemicalType: "Plant Growth Regulator",
    preHarvestIntervalInDays: 14,
    activeIngredient: "BACILLUS THURINGIENSIS",
    name: "GROCHEM DODINE",
    creationDate: "2017-09-21T04:53:24-12:00",
    modificationDate: null,
    deletionDate: "2019-08-28T05:12:33-12:00",
  },
  {
    id: "6181eb672807d4f08a07078f",
    chemicalType: "Tree Nutrition Fungicide",
    preHarvestIntervalInDays: "Up to 90% petal fall",
    activeIngredient: "LUFENURON",
    name: "PROVITA",
    creationDate: "2014-12-21T07:47:42-13:00",
    modificationDate: null,
    deletionDate: null,
  },
  {
    id: "6181eb67fe9b7ea0760aa818",
    chemicalType: "Surfactant",
    preHarvestIntervalInDays: 0,
    activeIngredient: "Nitrogen",
    name: "DRAGON 700WG",
    creationDate: "2015-01-12T12:22:28-13:00",
    modificationDate: "2021-01-30T06:59:25-13:00",
    deletionDate: null,
  },
  {
    id: "6181eb6740497afce260ebab",
    chemicalType: "Pesticide",
    preHarvestIntervalInDays: 0,
    activeIngredient: "PIRIMICARB",
    name: "DRAGON 700WG",
    creationDate: "2021-04-30T05:41:56-12:00",
    modificationDate: null,
    deletionDate: "2015-06-13T12:53:45-12:00",
  },
  {
    id: "6181eb67ef5c5b046e07fce9",
    chemicalType: "Pesticide",
    preHarvestIntervalInDays: 0,
    activeIngredient: "METAMITRON",
    name: "KEYSTREPTO",
    creationDate: "2016-06-23T03:25:34-12:00",
    modificationDate: null,
    deletionDate: "2018-07-06T03:41:38-12:00",
  },
  {
    id: "6181eb6790301ff891860d4c",
    chemicalType: "Pesticide",
    preHarvestIntervalInDays: "Up to 90% petal fall",
    activeIngredient: "BACILLUS THURINGIENSIS",
    name: "PROVITA",
    creationDate: "2019-07-29T05:08:50-12:00",
    modificationDate: null,
    deletionDate: null,
  },
  {
    id: "6181eb67f7157bb048b4df93",
    chemicalType: "Tree Nutrition Fungicide",
    preHarvestIntervalInDays: 0,
    activeIngredient: "INDOXACARB",
    name: "MIT-E-MEC",
    creationDate: "2018-06-11T02:55:32-12:00",
    modificationDate: null,
    deletionDate: "2016-11-19T04:17:28-13:00",
  },
  {
    id: "6181eb67efa2f282d1896693",
    chemicalType: "Pesticide",
    preHarvestIntervalInDays: 0,
    activeIngredient: "BUPIRIMATE",
    name: "ATS",
    creationDate: "2015-08-18T08:51:27-12:00",
    modificationDate: "2015-04-04T10:50:27-13:00",
    deletionDate: null,
  },
  {
    id: "6181eb67e0ef720985ee3a76",
    chemicalType: "Pesticide",
    preHarvestIntervalInDays: 0,
    activeIngredient: "PROPICONAZOLE + BAC + SALICYLIC ACID",
    name: "EZY-THIN",
    creationDate: "2016-08-11T09:52:41-12:00",
    modificationDate: "2017-03-13T06:40:45-13:00",
    deletionDate: null,
  },
  {
    id: "6181eb6748e90df5b5b3c30f",
    chemicalType: "Bactericides and fungicides",
    preHarvestIntervalInDays: 0,
    activeIngredient: "CHLORANTRANILIPROLE + ABAMECTIN",
    name: "PRODIGY",
    creationDate: "2016-04-02T12:47:17-13:00",
    modificationDate: "2017-02-23T04:10:04-13:00",
    deletionDate: null,
  },
];

function sleep(ms: number) {
  return new Promise((resolve) => setTimeout(resolve, ms));
}

const mockDataProvider = {
  getList: () => Promise.resolve({ data: [...mockChemicals.slice(0, 10)], total: mockChemicals.length }),
  create: () => Promise.resolve({ data: mockChemicals[0] }),
};

test("should show chemicals data on the page", async () => {
  render(<App dataContext={mockDataProvider} />);

  // const totalItems = getByTestId("total-item");
  await sleep(500);
  await screen.findByTestId("total-item");
  const totalItems = screen.getByTestId("total-item");
  expect(totalItems.textContent).toBe("There are 12 chemicals in total");

  const chemicalTypeHeader = await screen.findByText(/Chemical Type/i);
  expect(chemicalTypeHeader).toBeInTheDocument();
  const activeIngredientHeader = await screen.findByText(/Active ingredient/i);
  expect(activeIngredientHeader).toBeInTheDocument();
  const nameHeader = await screen.findByText(/Name/i);
  expect(nameHeader).toBeInTheDocument();
  const PHIHeader = await screen.findAllByText(/PHI/i);
  expect(PHIHeader.length).toBeGreaterThan(0);
});

test("chemicals should shown on the homepage", () => {
  render(<App dataContext={mockDataProvider} />);
  const chemicalElements = screen.getAllByText(/CHEMICALS/i);
  expect(chemicalElements.length).toBeGreaterThan(0);

  const titleChemical = screen.getByText("Chemicals");
  expect(titleChemical).toBeInTheDocument();
});

test("show create page when click to add new chemicals", async () => {
  render(<App dataContext={mockDataProvider} />);

  fireEvent.click(screen.getByText(/Add new chemicals/i));

  const titleCreateChemical = await screen.findByText("Create new chemical");
  expect(titleCreateChemical).toBeInTheDocument();
});
