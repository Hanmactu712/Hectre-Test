import { Button, Card, Col, Form, Input, notification, Row, Space, Spin, Typography } from "antd";
import { useNavigate } from "react-router-dom";
import { chemicalFields } from "../../data";
import { useCreateChemical } from "../../hooks/chemical";
import { IChemical } from "../../interfaces";

export const ChemicalCreate: React.FC = () => {
  const navigate = useNavigate();
  const { mutate, isLoading } = useCreateChemical<IChemical, IChemical>();

  const onFormFinish = (data: IChemical) => {
    mutate(
      {
        fields: chemicalFields,
        operation: "createChemical",
        variables: data,
      },
      {
        onSuccess: (data) => {
          notification.success({
            message: "Create chemical successfully",
          });
          navigate("/");
        },
      }
    );
  };

  return (
    <Card
      title={
        <Row justify="space-between" wrap align="middle">
          <Col>
            <Space>
              <Typography.Text className="chemical-list-title-name">Chemicals</Typography.Text>
              <Typography.Text className="chemical-list-title-total">Create new chemical</Typography.Text>
            </Space>
          </Col>
          <Col></Col>
        </Row>
      }
      extra={
        <Button type="link" onClick={() => navigate("/")}>
          {/* <PlusOutlined /> */}
          <Typography.Text className="chemical-list-add-text">{"List chemicals"}</Typography.Text>
        </Button>
      }
      className="list-chemical-card"
      headStyle={{
        margin: "0 12px",
        // padding: "20px 15px",
        borderBottom: "2px solid #DF1D00",
      }}
    >
      <Spin spinning={isLoading}>
        <Form layout="vertical" onFinish={onFormFinish}>
          <Form.Item
            name="chemicalType"
            label="Chemical Type"
            rules={[
              {
                required: true,
                message: "This is required field",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="activeIngredient"
            label="Active Ingredient"
            rules={[
              {
                required: true,
                message: "This is required field",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="name"
            label="Name"
            rules={[
              {
                required: true,
                message: "This is required field",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="preHarvestIntervalInDays"
            label="Pre Harvest Interval In Days"
            rules={[
              {
                required: true,
                message: "This is required field",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item>
            <Space style={{ float: "right" }}>
              <Button type="default" htmlType="submit" style={{ borderColor: "#df1d00" }}>
                Submit
              </Button>
              <Button type="default" htmlType="button" onClick={() => navigate("/")}>
                Cancel
              </Button>
            </Space>
          </Form.Item>
        </Form>
      </Spin>
    </Card>
  );
};
