import React from "react";
import {
  Button,
  Dialog,
  Card,
  CardHeader,
  CardBody,
  CardFooter,
  Typography,
  Input,
} from "@material-tailwind/react";
import Datepicker from "tailwind-datepicker-react"
import { IOptions } from "tailwind-datepicker-react/types/Options";
import { Formik} from 'formik'
import * as Yup from "yup";
import { CreateUserPermissionModel } from "../../../domain/home/model/CreateUserPermissionModel";

const  CreateUserPermissionModal = ({open,onHadler,handleOpen}:{open:boolean,handleOpen:() => void,onHadler: (request:CreateUserPermissionModel) => void;}) => {
  
  const [show, setShow] = React.useState<boolean>(false);
	const handleClose = (state: boolean) => {
		setShow(state)
	}
    const _onSave = (values:any) => {
        console.log(values);
        console.log(values.EmployeeForename)
        onHadler({
            EmployeeForename : values.EmployeeForename,
            EmployeeSurname : values.EmployeeSurname,
            PermissionDate:values.PermissionDate,
            
          } as CreateUserPermissionModel)
      };
   

    //todo:CHANGE
    const options = {
        title: "Select PermissionDate",
        autoHide: true,
        todayBtn: false,
        clearBtn: true,
        clearBtnText: "Clear",
        maxDate: new Date("2030-01-01"),
        minDate: new Date("1950-01-01"),
        datepickerClassNames: "top-12",
        language: "en",
        disabledDates: [],
        weekDays: ["Mo", "Tu", "We", "Th", "Fr", "Sa", "Su"],
        inputNameProp: "date",
        inputIdProp: "date",
        inputPlaceholderProp: "Select Date",
        inputDateFormatProp: {
            day: "numeric",
            month: "long",
            year: "numeric"
        }
    } as IOptions;
  return (
    <>
      <Dialog
        size="xs"
        open={open}
        handler={handleOpen}
        className="bg-transparent shadow-none"
      >
        <Formik
            initialValues={{ EmployeeSurname: "", EmployeeForename: "", PermissionDate:""}}
            validationSchema={Yup.object({
                EmployeeForename: Yup.string().required("Required"),
                EmployeeSurname: Yup.string().required("Required"),
                PermissionDate: Yup.string().required("Required"),
            })}
            onSubmit={(values) => _onSave(values)}
            >
            {({
                values,
                errors,
                touched,
                handleChange,
                handleBlur,
                handleSubmit,
            }) => (
                <form onSubmit={handleSubmit}>
                <Card className="mx-auto w-full max-w-[24rem]">
                    <CardHeader
                        variant="gradient"
                        color="indigo"
                        className="mb-4 grid h-28 place-items-center"
                    >
                        <Typography variant="h3" color="white">
                        User permissions
                        </Typography>
                    </CardHeader>
                    <CardBody className="flex flex-col gap-4">
                        <Input onChange={handleChange}
                        onBlur={handleBlur}
                            value={values.EmployeeForename} name="EmployeeForename" label="EmployeeForename" size="lg" crossOrigin={undefined}  error={(errors.EmployeeForename && touched.EmployeeForename ) ? true :false}/>
                        <Input onChange={handleChange}
                        onBlur={handleBlur}  value={values.EmployeeSurname} name="EmployeeSurname" label="EmployeeSurname" size="lg" crossOrigin={undefined}    error={(errors.EmployeeSurname && touched.EmployeeSurname ) ? true :false}
                        />
                        <Datepicker options={options}  show={show} setShow={handleClose} onChange={(e)=>{values.PermissionDate=e.toDateString()}}>
                            <div>
                            <div className="relative flex w-full max-w-[24rem]">
                                <Input
                                value={values.PermissionDate}
                                name="PermissionDate"
                                type="PermissionDate"
                                label="PermissionDate"
                                className="pr-20"
                                error={(errors.PermissionDate && touched.PermissionDate ) ? true :false}
                                containerProps={{
                                className: "min-w-0",
                                }} crossOrigin={undefined}/>
                                <Button
                                    size="sm"
                                    color={"gray"}
                                    onClick={()=>setShow(true)}
                                    className="!absolute right-1 top-1 rounded"
                                >
                                Select
                                </Button>
                            </div>
                            </div>
                        </Datepicker>
                    </CardBody>
                    <CardFooter className="pt-0">
                        <Button type="submit" variant="gradient"  fullWidth>
                        Save
                        </Button>
                    </CardFooter>
                </Card>
                </form>
            )}
            </Formik>
      </Dialog>
    </>
  );
}
export default CreateUserPermissionModal;