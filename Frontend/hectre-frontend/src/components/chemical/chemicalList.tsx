import { Button, Card, Col, Dropdown, Menu, Pagination, Row, Space, Spin, Table, Typography } from "antd";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { chemicalFields } from "../../data";
import { useListChemical } from "../../hooks/chemical";
import dropdownIcon from "../../icons/dropdown-icon.svg";
import leftIcon from "../../icons/left-icon.svg";
import rightIcon from "../../icons/right-icon.svg";
import { IChemical, IPagination } from "../../interfaces";

const ShowRecordOption = [
  {
    value: 10,
    label: "10 rows",
  },
  {
    value: 20,
    label: "20 rows",
  },
  {
    value: 50,
    label: "50 rows",
  },
];

const DEFAULT_CURRENT_PAGE = 1;
const DEFAULT_PAGE_SIZE = 10;

export const ChemicalList: React.FC = () => {
  const [currentPagination, setCurrentPagination] = useState<IPagination>({
    current: DEFAULT_CURRENT_PAGE,
    pageSize: DEFAULT_PAGE_SIZE,
  });
  const { data, isLoading, refetch } = useListChemical<IChemical>({
    operation: "chemicals",
    pagination: currentPagination,
    metaData: {
      fields: chemicalFields,
    },
    options: {
      enabled: false,
    },
  });
  const navigator = useNavigate();

  const onPaginationChange = (page: any) => {
    setCurrentPagination({ ...currentPagination, current: page });
  };

  const onShowRecordChange = (data: any) => {
    setCurrentPagination({ ...currentPagination, pageSize: Number.parseInt(data.key), current: DEFAULT_CURRENT_PAGE });
  };

  useEffect(() => {
    if (currentPagination) refetch();
  }, [currentPagination, refetch]);

  return (
    <Card
      title={
        <Row gutter={[24, 12]} justify="space-between" wrap align="middle">
          <Col>
            <Space wrap size={[24, 12]}>
              <Typography.Text className="chemical-list-title-name">Chemicals</Typography.Text>
              <Typography.Text className="chemical-list-title-total">{`There are ${data?.total} chemicals in total`}</Typography.Text>
            </Space>
          </Col>
          <Col flex={"auto"} style={{ textAlign: "right" }}>
            <Button type="link" onClick={() => navigator("/create")}>
              {/* <PlusOutlined /> */}
              <Typography.Text className="chemical-list-add-text">{"+ Add new chemicals"}</Typography.Text>
            </Button>
          </Col>
        </Row>
      }
      className="list-chemical-card"
      headStyle={{
        margin: "0 12px",
        borderBottom: "2px solid #DF1D00",
      }}
      actions={[
        <div className="chemical-list-card-action">
          <Row gutter={[36, 16]} wrap align="middle">
            <Col>
              <Pagination
                className="chemical-list-pagination"
                nextIcon={
                  <Button type="default" style={{ width: 32, padding: 0, verticalAlign: "top", borderRadius: 5 }}>
                    <img src={rightIcon} alt="next" width={24} height={24}></img>
                  </Button>
                }
                prevIcon={
                  <Button type="default" style={{ width: 32, padding: 0, verticalAlign: "top", borderRadius: 5 }}>
                    <img src={leftIcon} alt="prev" width={24} height={24}></img>
                  </Button>
                }
                showLessItems={true}
                current={currentPagination.current}
                pageSize={currentPagination.pageSize}
                showSizeChanger={false}
                total={data?.total}
                defaultCurrent={DEFAULT_CURRENT_PAGE}
                hideOnSinglePage
                onChange={onPaginationChange}
              ></Pagination>
            </Col>
            <Col flex={"auto"} style={{ textAlign: "right" }}>
              <Space>
                <span style={{ color: "#BDBDBD" }}>Show records</span>
                <Dropdown
                  className="chemical-list-show-records-dropdown"
                  overlay={
                    <Menu>
                      {ShowRecordOption &&
                        ShowRecordOption.map((item) => {
                          return (
                            <Menu.Item key={item.value} onClick={onShowRecordChange}>
                              <span>{item.label}</span>
                            </Menu.Item>
                          );
                        })}
                    </Menu>
                  }
                  trigger={["click"]}
                >
                  <span className="chemical-list-show-records-dropdown-text">
                    {(ShowRecordOption && ShowRecordOption.find((e) => e.value === currentPagination.pageSize)?.label) || "10 rows"}{" "}
                    <img src={dropdownIcon} alt="icon" width={24} height={24} />
                  </span>
                </Dropdown>
              </Space>
            </Col>
          </Row>
        </div>,
      ]}
    >
      <Spin spinning={isLoading}>
        <Table
          pagination={false}
          rowKey="id"
          scroll={{
            x: true,
          }}
          className="chemical-list-table"
          rowClassName={(record, index) => (index % 2 === 0 ? "chemical-table-alt-row" : "chemical-table-main-row")}
          dataSource={data?.data || []}
          //   dataSource={[
          //     {
          //       id: "6181eb674e68df4b5845299c",
          //       chemicalType: "Plant Growth Regulator",
          //       preHarvestIntervalInDays: "Up to 90% petal fall",
          //       activeIngredient: "SPINETORAM",
          //       name: "SERENADE OPTIMUM",
          //       creationDate: "2014-06-28T06:27:56-12:00",
          //       modificationDate: null,
          //       deletionDate: null,
          //     },
          //     {
          //       id: "6181eb67ce0fa0404dbf9752",
          //       chemicalType: "Surfactant",
          //       preHarvestIntervalInDays: 0,
          //       activeIngredient: "AMINOETHOXYVINYLGLYCINE (AVG)",
          //       name: "DIPEL DF",
          //       creationDate: "2014-10-19T12:41:11-13:00",
          //       modificationDate: null,
          //       deletionDate: "2019-05-30T03:11:26-12:00",
          //     },
          //     {
          //       id: "6181eb678556f8c194c22d8b",
          //       chemicalType: "Plant Growth Regulator",
          //       preHarvestIntervalInDays: 14,
          //       activeIngredient: "BACILLUS THURINGIENSIS",
          //       name: "GROCHEM DODINE",
          //       creationDate: "2017-09-21T04:53:24-12:00",
          //       modificationDate: null,
          //       deletionDate: "2019-08-28T05:12:33-12:00",
          //     },
          //   ]}
          columns={[
            {
              key: "chemicalType",
              dataIndex: "chemicalType",
              title: "Chemical Type",
              width: "25%",
              className: "chemical-list-table-chemical-type-column",
            },
            {
              key: "activeIngredient",
              dataIndex: "activeIngredient",
              title: "Active Ingredient",
              width: "25%",
            },
            {
              key: "name",
              dataIndex: "name",
              title: "Name",
              width: "25%",
            },
            {
              key: "preHarvestIntervalInDays",
              dataIndex: "preHarvestIntervalInDays",
              title: "PHI (DAYS)",
              width: "25%",
            },
          ]}
        ></Table>
      </Spin>
    </Card>
  );
};
