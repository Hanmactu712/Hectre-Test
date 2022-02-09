import { ChemicalCreate, ChemicalList } from "../components/chemical";
import { IRoute } from "../interfaces";

export const appRoutes: IRoute[] = [
  {
    id: "1.0",
    path: "/",
    name: "Chemicals",
    element: <ChemicalList />,
  },
  {
    id: "1.1",
    path: "/create",
    name: "Create Chemical",
    element: <ChemicalCreate />,
  },
];
