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
  Checkbox,
} from "@material-tailwind/react";
import Datepicker from "tailwind-datepicker-react"
import { IOptions } from "tailwind-datepicker-react/types/Options";
import { Formik, Field, Form, ErrorMessage } from 'formik'
import * as Yup from "yup";

const  CreateUserPermissionModal = () => {
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen((cur) => !cur);
  const [show, setShow] = React.useState<boolean>(false);
	const handleChange = (selectedDate: Date) => {
		console.log(selectedDate)
	}
	const handleClose = (state: boolean) => {
		setShow(state)
	}
    const _onSave = (values:any) => {
        console.log(values);
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
      <Button onClick={handleOpen}>Sign In</Button>
      <Dialog
        size="xs"
        open={open}
        handler={handleOpen}
        className="bg-transparent shadow-none"
      >
  <Formik
      initialValues={{ EmployeeSurname: "", EmployeeForename: ""}}
      validationSchema={Yup.object({
        EmployeeForename: Yup.string().required("Required"),
        EmployeeSurname: Yup.string().required("Required"),
     
      })}
      onSubmit={(values) => _onSave(values)}
    >
      {({
        values,
        errors,
        touched,
        handleChange,
        setFieldValue,
        handleBlur,
        handleSubmit,
        isSubmitting
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
             <Datepicker  options={options} onChange={handleChange} show={show} setShow={handleClose} />
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